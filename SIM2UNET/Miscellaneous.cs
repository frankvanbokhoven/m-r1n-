/*------------------------------------------------------------------------------
Copyright           : Copyright (c) Alenia Marconi Systems 2002,
                      Integrated Systems Division, Donibristle.

                      The copyright of this software is the property of Alenia
                      Marconi Systems Ltd.

                      The software is supplied by Alenia Marconi Systems on the 
                      express terms that it is to be treated as confidential, and 
                      that it may not be copied, used or disclosed to others for 
                      any purpose except as authorised in writing by this company.

Project Title       : UNET

CSCI                : VCS
------------------------------------------------------------------------------
Module Specification : 

UNET-MS-090 EW Trainer To TroyCommunications System Interface Specification
UNET-MS-104 Voice Communication System Communications Interface Panel MMI
UNET-MS-134 VCS Interface PDUs

------------------------------------------------------------------------------
Module Description   : 

This module contains a number of class members which were not directly responsible
for exercise management (dealt with by VCSExerciseManagement module) or VCS interface message 
handling (dealt with by VCSInterfaceManagement module).
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//# include "stdafx.h"
//# include "mmsystem.h"
//# include <sys/timeb.h>

//        // Comms Component headers
//# include "CommsComponent.h"
//# include "systemcomponent.h"
namespace SIM2UNET
{
    public class Miscellaneous
    {

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : DetectStatusChange
        // Description: Detects a change in the Position Status structure contained within 
        //							the VCS A_SC_RU_ALIVE message.  Returns true if change detected or
        //							false otherwise
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F594013C
        public bool DetectStatusChange(char pInputData, bool bGoodVcsControlStatus, bool bGoodVcsNodeStatus)
        {

            bool reply = false;
            int max_byte_count;
            char pOutputData = &m_tSystemStatus.cSystemPC;

            int bPoweredControlPCCount = 0;
            int bPoweredNodeCount = 0;

            max_byte_count = 6;

            for (byte = 0; byte < max_byte_count; ++byte, pInputData++, pOutputData++)
            {
                if (1 == byte)          //Not dual processor system
                {
                    pOutputData--;
                    continue;
                }
                else if (5 == byte) //Number of positions reported
                {
                    max_byte_count += *pInputData;
                    pOutputData--;
                    continue;
                }

                if (*pInputData & 1)
                {
                    if (byte < 5)
                        bPoweredControlPCCount++;
                    else
                        bPoweredNodeCount++;
                }

                if (*pOutputData != *pInputData)
                {

                    *pOutputData = *pInputData;
                    reply = true;
                }
            }

            if (TOTAL_NUM_CONTROL_PC == bPoweredControlPCCount)

                *bGoodVcsControlStatus = true;

            if (m_sTotalNumNodes == bPoweredNodeCount)

                *bGoodVcsNodeStatus = true;

            return reply;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : UpdateNodeStatus
        // Description: Updates the node management structure (m_tVcsNodeManage) which is
        //							used to detect when a desk running within an exercise has failed 
        //							enabling that desk to be logged back in if it comes back on-line
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F59401A0
        public void UpdateNodeStatus()
        {

            char pNodeStatus = m_tSystemStatus.cCommsNode;
            int nNodeOK;
            short sNodeID, sRoleID;
            int nExID;

            /* Have to map between allocated desk numbers and node numbers */
            for (sNodeID = 1; sNodeID <= m_sTotalNumNodes; sNodeID++)
            {
                nNodeOK = (int)((*pNodeStatus++ & 3) == 3); //Position ok & active in exercise flag

                if (nNodeOK)
                {   /* Node is now active */
                    switch (m_tVcsNodeManage[sNodeID].eNodeStatus)
                    {
                        case NODE_ACTIVE:
                        case NODE_DEAD:
                            m_tVcsNodeManage[sNodeID].eNodeStatus = NODE_ACTIVE;
                            break;

                        case NODE_FAILED:
                            m_tVcsNodeManage[sNodeID].eNodeStatus = NODE_ACTIVE;
                            /* Login roles required by this node */
                            for (nExID = 0; nExID < m_sTotalExerciseCount; nExID++)
                            {
                                for (sRoleID = 0; sRoleID < m_pVcsExManage[nExID].sRoleCount; sRoleID++)
                                {
                                    if (((sNodeID + DESK_OFFSET) == m_pVcsExManage[nExID].Roles[sRoleID].sPhysicalNode) &&
                                            (LOGIN_FAILED == m_pVcsExManage[nExID].Roles[sRoleID].eLoginStatus))
                                    {
                                        UpdateExerciseStatus(nExID, CScenarioComponent::LOADING, "", 0, m_pVcsExManage[nExID].Roles[sRoleID].sPhysicalNode);

                                        if (nExID == IL_IDX)
                                        {   /* Login to IL Session */
                                            if (LoginIlExercise(sRoleID))
                                            {
                                                SendLoggerMessage(T_AMS_COMMS, IL_IDX, "Problem logging back into IL session");
                                                return;
                                            }
                                        }
                                        else
                                        {   /* Login to Exercise */
                                            if (LoginVcsExercise(nExID, sRoleID, true))
                                            {
                                                SendLoggerMessage(T_AMS_COMMS, nExID, "Problem logging back into exercise");
                                                return;
                                            }
                                        }
                                        /* Send message */
                                        if (READY_CONNECT == m_tVcsIfManage.eVCS_LinkState)
                                        {
                                            m_pVcsExManage[nExID].Roles[sRoleID].eLoginStatus = LOGIN_ACTIVE;
                                            SendMsgToVCS(nExID, m_pVcsExManage[nExID].pMsgQueue, eTHREAD_EXM);
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {   /* Node has now failed */
                    switch (m_tVcsNodeManage[sNodeID].eNodeStatus)
                    {
                        case NODE_DEAD:
                        case NODE_FAILED:
                            break;
                        case NODE_ACTIVE:
                            m_tVcsNodeManage[sNodeID].eNodeStatus = NODE_FAILED;
                            /* Need to register roles which are no longer logged in */
                            for (nExID = 0; nExID < m_sTotalExerciseCount; nExID++)
                                for (sRoleID = 0; sRoleID < m_pVcsExManage[nExID].sRoleCount; sRoleID++)
                                    if (((sNodeID + DESK_OFFSET) == m_pVcsExManage[nExID].Roles[sRoleID].sPhysicalNode) &&
                                            (LOGIN_NOT != m_pVcsExManage[nExID].Roles[sRoleID].eLoginStatus))
                                        m_pVcsExManage[nExID].Roles[sRoleID].eLoginStatus = LOGIN_FAILED;

                            break;
                    }
                }
            }
        }

        public void SendLoggerMessage()
        {

        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : AddMessageToQueue
        // Description: Allocates memory for a message to be sent to the VCS and adds pointer
        //							to memory block to the appropriate exercise queue.
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F59402C2
        public QueuedMsg_t AddMessageToQueue(int VIC_ExNo, short sFunctionID)
        {
            string newMsg, lastMsg;

            newMsg = new QueuedMsg_t;
            if (!newMsg)
            {
                SendLoggerMessage(T_AMS_MISC, VIC_ExNo, "No memory space for adding message to queue");
                return NULL;
            }
            newMsg->pNextMsg = NULL;

            if (NO_EXERCISE == VIC_ExNo)
            {
                if (!(lastMsg = m_tVcsGenManage.pMsgQueue))
                {
                    m_tVcsGenManage.pMsgQueue = newMsg;
                    return newMsg;
                }
            }
            else
            {
                if (!(lastMsg = m_pVcsExManage[VIC_ExNo].pMsgQueue))
                {
                    m_pVcsExManage[VIC_ExNo].pMsgQueue = newMsg;
                    return newMsg;
                }
            }

            while (lastMsg->pNextMsg)
                lastMsg = lastMsg->pNextMsg;

            lastMsg->pNextMsg = newMsg;

            return newMsg;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : ClearMsgQueue
        // Description: Clears all messages in an exercise queue, freeing up memory.
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5940308
        public void ClearMsgQueue(int VIC_ExNo)
        {
            QueuedMsg_t pNextQueuedMsg;

            if (NO_EXERCISE == VIC_ExNo)
                return;
            else if (GEN_IDX == VIC_ExNo)
            {
                while (m_tVcsGenManage.pMsgQueue)
                {   /* Free unsent messages */
                    pNextQueuedMsg = m_tVcsGenManage.pMsgQueue.pNextMsg;
                    free(m_tVcsGenManage.pMsgQueue);
                    m_tVcsGenManage.pMsgQueue = pNextQueuedMsg;
                }

                m_tVcsGenManage.nMsgCnt = 0;
                m_tVcsGenManage.eMsgStatus = MESSAGE_IDLE;
            }
            else
            {
                while (m_pVcsExManage[VIC_ExNo].pMsgQueue)
                {   /* Free unsent messages */
                    pNextQueuedMsg = m_pVcsExManage[VIC_ExNo].pMsgQueue->pNextMsg;
                    free(m_pVcsExManage[VIC_ExNo].pMsgQueue);
                    m_pVcsExManage[VIC_ExNo].pMsgQueue = pNextQueuedMsg;
                }

                m_pVcsExManage[VIC_ExNo].nMsgCnt = 0;
                m_pVcsExManage[VIC_ExNo].eMsgStatus = MESSAGE_IDLE;
            }
            return;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : SendNextQueuedMessage
        // Description: Finds if there is another message for VCS awaiting transmission
        //							and if so sends it
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F5940344
        public void SendNextQueuedMessage()
        {
            int VIC_ExNo;

            if (NO_EXERCISE != (VIC_ExNo = FindMsgToVCS()))
            {
                if (GEN_IDX == VIC_ExNo)
                    SendMsgToVCS(GEN_IDX, m_tVcsGenManage.pMsgQueue, eTHREAD_IFM);
                else
                    SendMsgToVCS(VIC_ExNo, m_pVcsExManage[VIC_ExNo].pMsgQueue, eTHREAD_IFM);
            }
            else
                m_tVcsIfManage.eVCS_LinkState = READY_CONNECT;

            return;
        }


        ////////////////////////////////////////////////////////////////////////////////
        // Function   : AppendIntStr
        // Description: Converts an integer to string format (with variable quantity of 
        //							leading zeros) and appends this string to another string 
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5940236
        public void AppendIntStr(char szSource, int nValue, int nPadding)
        {
            int nThousands, nHundreds, nTens, nUnits;
            char szTemp[4];

            int idx = 0;

            /* only handling positive integers up to 9999 at present */
            if ((nValue > 9999) || (nPadding < 0) || (nPadding > 4))
                return;

            nThousands = nValue / 1000;
            nValue -= (nThousands * 1000);

            nHundreds = nValue / 100;
            nValue -= (nHundreds * 100);

            nTens = nValue / 10;
            nUnits = nValue % 10;

            if ((nThousands) || (nPadding > 3))
                szTemp[idx++] = '0' + nThousands;

            if (nThousands || nHundreds || (nPadding > 2))
                szTemp[idx++] = '0' + nHundreds;

            if (nThousands || nHundreds || nTens || (nPadding > 1))
                szTemp[idx++] = '0' + nTens;

            szTemp[idx++] = '0' + nUnits;

            szTemp[idx] = '\0';

            strcat(szSource, szTemp);

            return;
        }


        ////////////////////////////////////////////////////////////////////////////////
        // Function   : GetTimeStr
        // Description: Converts a time stamp (type double) into a string of format HH:MM:SS:mmm
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F59401BE
        public void GetTimeStr(char szTime, double dScenarioTime)
        {
            int nHours, nMins, nSecs, nMSecs;

            nHours = (int)(dScenarioTime / 3600.0);
            dScenarioTime -= (double)(nHours * 3600);
            nMins = (int)(dScenarioTime / 60.0);
            dScenarioTime -= (double)(nMins * 60);
            nSecs = (int)dScenarioTime;
            nMSecs = (int)((dScenarioTime - (double)nSecs) * 1000.0);

            strcpy(szTime, "");
            AppendIntStr(szTime, nHours, Pad_2);
            strcat(szTime, ":");
            AppendIntStr(szTime, nMins, Pad_2);
            strcat(szTime, ":");
            AppendIntStr(szTime, nSecs, Pad_2);
            strcat(szTime, ":");
            AppendIntStr(szTime, nMSecs, Pad_3);

            return;

        }


        ////////////////////////////////////////////////////////////////////////////////
        // Function   : AppendCurrentTimeStr
        // Description: Appends the current date and time to a supplied string
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5940204
        public void AppendCurrentTimeStr(char szIn)
        {

            _timeb tExtTime;
            DateTime tTime;
            char szTimeStr[25];
            char szTemp[5];


            _ftime(&tExtTime);
            tTime = DateTime.Now;// gmtime(&tExtTime.time);

            string.format(szTimeStr, "%d_%d%_%d %d_%d_%d_",
                                             tTime->tm_mday,
                                             tTime->tm_mon + 1,
                                             tTime->tm_year + 1900,
                                             tTime->tm_hour,
                                             tTime->tm_min,
                                             tTime->tm_sec);



            sprintf(szTemp, "%d", tExtTime.millitm);

            strcat(szTimeStr, szTemp);

            strcat(szIn, szTimeStr);

            return;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : FindVcsExercise
        // Description: Finds the AMS exercise number associated with a VCS exercise number
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5940290
        public short FindVcsExercise(UInt16 usVcsExerciseNo)
        {
            int i;

            for (i = 0; i < m_sTotalExerciseCount; i++)
            {
                if (m_pVcsExManage[i].sVcsExerciseNumber == (short)usVcsExerciseNo)
                    return i;
            }

            SendLoggerMessage(T_AMS_MISC, usVcsExerciseNo, "Fatal error - unknown VCS exercise");

            return NO_EXERCISE; //will cause crash

        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : UpdateExerciseStatus
        // Description: Updates status message in system component
        //							Status bytes sequenced:	m_sTotalNumInstructors x Instructor, 
        //																			[m_sTotalNumTrainees] x Trainee, 
        //																			1 x Debrief 
        ////////////////////////////////////////////////////////////////////////////////

        public void UpdateExerciseStatus(int VIC_ExNo, int nState, char szErrorInfo, short sLogicalNodeID, short sPhysicalNodeID)
        {
            int bUpdateStatus;
            int bEnableTransmit;
            int n;
            int nPhysicalNodeID = -1;

            Byte cLocalVcsNodeStatus[MAX_NUM_NODES];

#if _VIC_CMND_STATUS_TRACE
		char tempStr[100];
#endif

            bUpdateStatus = strcmp(szErrorInfo, "No_Status_Update");
            bEnableTransmit = strcmp(szErrorInfo, "No_Transmit");

            if (bUpdateStatus)
            {
                if (EXERCISE_REPLAY == m_pVcsExManage[VIC_ExNo].eLoadType)
                {
                    //Associate supplied status with replay node
                    if (CScenarioComponent::RUNNING_BACKWARDS == nState)
                        m_tVcsIfManage.cVcsNodeStatus[sPhysicalNodeID] = CScenarioComponent::UNINITIALISED;
                    else if (CScenarioComponent::NON_OPERATIONAL != m_tVcsIfManage.cVcsNodeStatus[m_sTotalNumNodes - 1])
                        m_tVcsIfManage.cVcsNodeStatus[m_sTotalNumNodes - 1] = (BYTE)nState;

#if _VIC_CMND_STATUS_TRACE
				sprintf(tempStr, "VIC State [EXERCISE %d][Node %d] = ", VIC_ExNo, m_sDebriefNode + DESK_OFFSET);
#endif
                }
                else
                {
                    // Deal with status depending on how node(s) defined 

                    /* Update node status */
                    if (sLogicalNodeID)
                    {
                        //*********************** UPDATE BY LOGICAL DESK NUMBER ***********************

                        /* If logical node information is provided then nState relates to node instead of all nodes in exercise */
                        for (n = 0; n < m_pVcsExManage[VIC_ExNo].sRoleCount; n++)
                        {
                            if (sLogicalNodeID == m_pVcsExManage[VIC_ExNo].Roles[n].sLogicalNode)
                            {
                                nPhysicalNodeID = m_pVcsExManage[VIC_ExNo].Roles[n].sPhysicalNode;

                                /* Debug print */
#if _VIC_CMND_STATUS_TRACE
							sprintf(tempStr, "VIC State [EXERCISE %d][Node %d] = ", VIC_ExNo, nPhysicalNodeID);
#endif

                                nPhysicalNodeID -= DESK_OFFSET;
                                break;
                            }
                        }

                        if (-1 == nPhysicalNodeID)
                            return;

                        /* Convert physical node to status node then update appropriate status word */
                        if (CSystemComponent::SPARK_INSTRUCTOR == m_pVcsExManage[VIC_ExNo].Roles[n].sRoleType)
                        {   /* Instructor */
                            nPhysicalNodeID -= m_sTotalNumTrainees + 1;

                            /* Instructor nodes only use NON_OPERATIONAL and UNINITIALISED status  */
                            if ((CScenarioComponent::UNINITIALISED == nState) || (CScenarioComponent::NON_OPERATIONAL == nState))
                            {
                                if (CScenarioComponent::NON_OPERATIONAL != m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID])
                                    m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID] = (BYTE)nState;
                            }
                            else
                            {
                                if ((VIC_ExNo > 0) && (VIC_ExNo <= 8))
                                {
                                    m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID] = (BYTE)CScenarioComponent::UNINITIALISED;

                                    if (CScenarioComponent::FAULT == nState)
                                        m_tVcsIfManage.cVcsInstructorNodeExerciseStatus[nPhysicalNodeID] |= (VCS_INSTRUCTOR_FAULT << (VIC_ExNo - 1));
                                    else
                                        m_tVcsIfManage.cVcsInstructorNodeExerciseStatus[nPhysicalNodeID] |= (VCS_INSTRUCTOR_NO_FAULT << (VIC_ExNo - 1));
                                }
                            }
                        }
                        else
                        {   /* Trainee */
                            nPhysicalNodeID += m_sTotalNumInstructors - 1;
                            if (CScenarioComponent::NON_OPERATIONAL != m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID])
                                m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID] = (BYTE)nState;
                        }
                    }
                    else if (sPhysicalNodeID)
                    {
                        //*********************** UPDATE BY PHYSICAL DESK NUMBER ***********************

                        /* If physical node information is provided then nState relates to node instead of all nodes in exercise */
                        /* Debug print */
#if _VIC_CMND_STATUS_TRACE
					sprintf(tempStr, "VIC State [EXERCISE %d][Node %d] = ", VIC_ExNo, sPhysicalNodeID);
#endif

                        sPhysicalNodeID -= DESK_OFFSET;


                        /* Convert physical node to status node then update appropriate status word */
                        if (sPhysicalNodeID > m_sTotalNumRoles)
                            sPhysicalNodeID -= 1;                                                       //Debrief
                        else if (sPhysicalNodeID > m_sTotalNumTrainees)
                            sPhysicalNodeID -= (m_sTotalNumTrainees + 1);       //Instructor
                        else
                            sPhysicalNodeID += m_sTotalNumInstructors - 1;  //Trainee

                        /* RUNNING_BACKWARDS indicates that PC has come on-line and enables non-operational state to change
                             top uninitialised otherwise, non-operational state will not update to new status.  The RUNNING_BACKWARDS
                             state is only used with status update by physical desk number				*/
                        if (CScenarioComponent::RUNNING_BACKWARDS == nState)
                            m_tVcsIfManage.cVcsNodeStatus[sPhysicalNodeID] = CScenarioComponent::UNINITIALISED;
                        else if (CScenarioComponent::NON_OPERATIONAL != m_tVcsIfManage.cVcsNodeStatus[sPhysicalNodeID])
                            m_tVcsIfManage.cVcsNodeStatus[sPhysicalNodeID] = (BYTE)nState;
                    }
                    else
                    {
                        //*********************** UPDATE ALL DESKS IN EXERCISE ***********************

                        /* Update scenario state - used for status update on login after desk power cycle */
                        m_pVcsExManage[VIC_ExNo].cScenarioState = (BYTE)nState;

                        /* All nodes in exercise have same status */
                        for (n = 0; n < m_pVcsExManage[VIC_ExNo].sRoleCount; n++)
                        {
                            nPhysicalNodeID = m_pVcsExManage[VIC_ExNo].Roles[n].sPhysicalNode - DESK_OFFSET;

                            /* Convert physical node to status node then update appropriate status word */
                            if (CSystemComponent::SPARK_INSTRUCTOR == m_pVcsExManage[VIC_ExNo].Roles[n].sRoleType)
                            {   /* Instructor */
                                nPhysicalNodeID -= m_sTotalNumTrainees + 1;

                                /* Instructor nodes only use NON_OPERATIONAL and UNINITIALISED status  */
                                if ((CScenarioComponent::UNINITIALISED == nState) || (CScenarioComponent::NON_OPERATIONAL == nState))
                                {
                                    if (m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID] != CScenarioComponent::NON_OPERATIONAL)
                                    {
                                        m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID] = (BYTE)nState;
                                        m_tVcsIfManage.cVcsInstructorNodeExerciseStatus[nPhysicalNodeID] &= (VCS_INSTRUCTOR_NO_FAULT << (VIC_ExNo - 1));
                                    }
                                }
                                else
                                {
                                    if ((VIC_ExNo > 0) && (VIC_ExNo <= MAX_NUM_DESKS))
                                    {
                                        if (CScenarioComponent::FAULT == nState)
                                            m_tVcsIfManage.cVcsInstructorNodeExerciseStatus[nPhysicalNodeID] |= (VCS_INSTRUCTOR_FAULT << (VIC_ExNo - 1));
                                        else
                                            m_tVcsIfManage.cVcsInstructorNodeExerciseStatus[nPhysicalNodeID] &= (VCS_INSTRUCTOR_NO_FAULT << (VIC_ExNo - 1));
                                    }
                                }
                            }
                            else
                            {   /* Trainee */
                                nPhysicalNodeID += m_sTotalNumInstructors - 1;
                                if (CScenarioComponent::NON_OPERATIONAL != m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID])
                                    m_tVcsIfManage.cVcsNodeStatus[nPhysicalNodeID] = (BYTE)nState;
                            }
                        }

                        /* Debug print */
#if _VIC_CMND_STATUS_TRACE
					sprintf(tempStr, "VIC State [EXERCISE %d][All nodes] = ", VIC_ExNo);
#endif
                    }
                }

                /* Debug print */
#if _VIC_CMND_STATUS_TRACE
			switch (nState)
			{
				case CScenarioComponent::UNINITIALISED :
					strcat(tempStr, "UNINITIALISED");
					break;

				case CScenarioComponent::NON_OPERATIONAL:
					strcat(tempStr, "NON_OPERATIONAL");
					break;

				case CScenarioComponent::FAULT:
					strcat(tempStr, "FAULT");
					break;

				case CScenarioComponent::LOADING:
					strcat(tempStr, "LOADING");
					break;

				case CScenarioComponent::LOADED:
					strcat(tempStr, "LOADED");
					break;

				case CScenarioComponent::STARTING:
					strcat(tempStr, "STARTING");
					break;

				case CScenarioComponent::STOPPED:
					strcat(tempStr, "STOPPED");
					break;

				case CScenarioComponent::RESTORING:
					strcat(tempStr, "RESTORING");
					break;

				case CScenarioComponent::RUNNING:
					strcat(tempStr, "RUNNING");
					break;

				case CScenarioComponent::FROZEN:
					strcat(tempStr, "FROZEN");
					break;

				case CScenarioComponent::RUNNING_BACKWARDS:
					strcat(tempStr, "UNINITIALISED");
					break;

				case CScenarioComponent::TRAINEE_CHANGED:
					strcat(tempStr, "TRAINEE_CHANGED");
					break;

				default:
					strcat(tempStr, "UNKNOWN");
					break;
			}
 
			strcat(tempStr, "\n"); 
			Console.WriteLine(tempStr); 

#endif

                /* Handle fault situation */
                if (CScenarioComponent::FAULT == nState)
                {
                    SendLoggerMessage(T_AMS_MISC, VIC_ExNo, szErrorInfo);

                    if (m_pVcsExManage[VIC_ExNo].eED_State < ED_READY)
                    {
                        if (IL_IDX == VIC_ExNo)
                            m_tVcsGenManage.bStopAllExerciseProcessing = true;
                        else
                        {
                            m_pVcsExManage[VIC_ExNo].bStopAllExerciseProcessing = true;
                            m_pVcsExManage[VIC_ExNo].eED_State = ED_UNKNOWN;
                        }
                        // Do not send pending messages for this exercise
                        ClearMsgQueue(VIC_ExNo);
                    }
                }

            }

            if (bEnableTransmit)
            {
                /* Update local status structure - instructors */
                for (n = 0; n < m_sTotalNumInstructors; n++)
                {
                    cLocalVcsNodeStatus[n] = m_tVcsIfManage.cVcsNodeStatus[n];
                    if (m_tVcsIfManage.cVcsInstructorNodeExerciseStatus[n])
                        cLocalVcsNodeStatus[n] = CScenarioComponent::FAULT;
                }

                /* Update local status structure - trainees & debrief */
                for (; n < m_sTotalNumNodes; n++)
                    cLocalVcsNodeStatus[n] = m_tVcsIfManage.cVcsNodeStatus[n];

                /* Send IL messages as exercise MAX_EXERCISE_COUNT */
                VIC_ExNo = (VIC_ExNo == IL_IDX) ? MAX_EXERCISE_COUNT : VIC_ExNo;
                VIC_ExNo = (VIC_ExNo == REPLAY_IDX) ? m_sAmsReplayExerciseNumber : VIC_ExNo;

                if (m_bShutdownRequested)
                    m_pSystem->SetSystemState(false, CScenarioComponent::READY_TO_POWER_OFF, VIC_ExNo, cLocalVcsNodeStatus);
                else
                    m_pSystem->SetSystemState(m_tVcsIfManage.bVcsControlPcStatus, m_tVcsIfManage.bVcsControlPcStatus, VIC_ExNo, cLocalVcsNodeStatus);

#if _VCS_NODE_STATUS_TRACE
			sprintf (m_szTrace, "VCS STATUS UPDATE SENT - Instructors: ");
			for (n = 0; n<m_sTotalNumInstructors; n++)
			{
				AppendIntStr(m_szTrace, cLocalVcsNodeStatus[n], Pad_0);
				strcat (m_szTrace, " ");
			}
			
			strcat (m_szTrace, "Trainees: ");

			for (; n<m_sTotalNumRoles; n++)
			{
				AppendIntStr(m_szTrace, cLocalVcsNodeStatus[n], Pad_0);
				strcat (m_szTrace, " ");
			}
		
			strcat(m_szTrace, "Debrief: ");
			AppendIntStr(m_szTrace, cLocalVcsNodeStatus[n], Pad_0);
			strcat(m_szTrace, "\n");
			Console.WriteLine("");

#endif

            }
        }


        ////////////////////////////////////////////////////////////////////////////////
        // Function   : CheckShutdownRequest
        // Description: Checks for 'PrepareToPowerOff' command in startupinfo.ini file 
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F594036D
        public void CheckShutdownRequest(uint uID, uint uMsg, dword pOwner, DWORD dw1, DWORD dw2)
        {
            static const char szStartupIni[] = "s:\\startup\\startupinfo.ini";
            char szCurrentCommand[100];

            pCommsComp = reinterpret_cast<CCommsComponent*>(pOwner);
            ASSERT(pCommsComp);

            /* Get ini file detail */
            GetPrivateProfileString(pCommsComp->m_szPCName, "Command", "", szCurrentCommand, 99, szStartupIni);

            if (!strcmp(szCurrentCommand, "PrepareToPowerOff"))
            {
                /* Initiate shutdown */
                pCommsComp->SendShutdownRequest();

                /* Alter ini file to prevent repeat shutdown initiation */
                WritePrivateProfileString(pCommsComp->m_szPCName, "Command", "PowerOffPreparationComplete", szStartupIni);
            }

            return;
        }


        ////////////////////////////////////////////////////////////////////////////////
        // Function   : TraceVcs
        // Description: Enables trace text to be written to the eventlog 
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5950011
        public void TraceVcs(char pcTraceString)
        {

            if (strcmp(pcTraceString, ""))
                strcpy(m_szTrace, pcTraceString);

            if (m_sparkDebug)
            {

                //m_pEL->AddEvent(m_szTrace, eEventInformation);
                //m_pEL->AddEvent(m_szTrace, eEventWarning);
                //m_pEL->AddEvent(m_szTrace, eEventError);
                //m_pEL->AddEvent(m_szTrace, eEventSignificant);
                m_pEL->AddEvent(&m_szTrace[0], eEventDebug);
            }
            else
                TRACE(m_szTrace);


            return;
        }

    }
}
