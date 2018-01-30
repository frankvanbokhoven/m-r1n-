/*------------------------------------------------------------------------------
Project Title       : UNET
CSCI                : VCS
Classification      : Unclassified
Language            : C++
Operating System    : Microsoft Windows 2000
Module Description   : 
This module handles the majority of messages sent to the VCS and all messages 
received from the VCS. For the most part the interface is half-duplex whereby
a response must be received to the previously transmitted message before the 
next message can be sent. Although the VCS can handle full duplex messages there
are instances (such as exercise specification and logging in the first instructor) 
in which it is not possible to determine the associated message exercise or which 
can cause race hazards within the VCS making it sensible to use the half-duplex
approach. 

The module contains a class member (SendMsgToVCS) to send messages to the VCS. Once 
a message is sent the link status changes from READY_CONNECT to BUSY_CONNECT and no 
messages added to any queue should be transmitted. Upon receiving the appropriate 
response the module will remove the outgoing message from the queue and send another 
queued message. If there are no queued messages left the link status will change back
to READY_CONNECTand the first message added to any exercise queue will be transmitted
by the CommsComponent module using the SendMsgToVCS member.
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using UNET_Classes;

//# include "stdafx.h"
//# include "process.h"    
//# include "ctype.h"

//# include "CommsComponent.h"
//# include "systemcomponent.h"

namespace SIM2VOIP
{
    public class InterfaceManagement
    {

        /* Socket Handling */
        public string serverAddr;// sockaddr_in serverAddr;
        public System.Net.Sockets.Socket clientSocket;

        static int Search_ExNo = 0;

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : ConnectToVCS
        // Description: Creates a client socket for connection to VCS
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F5930180
        //        public void ConnectToVCS(void pOwner)
        //        {
        //            int nConnection = -1;
        //            u_long BLOCKING = 0;
        //            u_long UNBLOCKING = 1;

        //            bool bSendError1 = true;
        //            bool bSendError2 = true;

        //            int i = 0, n = 0;

        //            //struct linger  mylinger;

        //            pCommsComp = reinterpret_cast<CCommsComponent*>(pOwner);
        //            ASSERT(pCommsComp);

        //            /* Exit if connection already valid */
        //            if ((READY_CONNECT == pCommsComp->m_tVcsIfManage.eVCS_LinkState) || (BUSY_CONNECT == pCommsComp->m_tVcsIfManage.eVCS_LinkState))
        //                return;

        //            /* Prevent messages other that I_SC_RU_ALIVE being sent to VCS */
        //            pCommsComp->m_bVCS_Startup = true;
        //            pCommsComp->m_bResetComplete = false;

        //            /* Create a socket for VCS Interface */
        //            clientSocket = socket(AF_INET, SOCK_STREAM, 0); //TCP
        //            ioctlsocket(clientSocket, FIONBIO, &BLOCKING);          //can use unblocking with WSAAsyncSelect

        //            serverAddr.sin_family = AF_INET;

        //            /* Define server address */
        //            serverAddr.sin_addr.s_addr = inet_addr(pCommsComp->m_szSERVER_ADDRESS);
        //            serverAddr.sin_port = htons(pCommsComp->m_usSERVER_PORT_NUMBER);

        //            /* Loop until connection made */
        //            while (nConnection < 0)
        //            {
        //                if (INVALID_SOCKET != clientSocket)
        //                {
        //                    /* Use this code if not wanting default SO_DONTLINGER connection termination x/	
        //                    mylinger.l_onoff = 1;		//LINGER
        //                    mylinger.l_linger = 0;	//No time

        //                    if (setsockopt(clientSocket, SOL_SOCKET, SO_LINGER, (const char FAR *) &mylinger, sizeof(mylinger)))
        //                        pCommsComp->m_nErrorCode = pCommsComp->GetWSAError(WSAGetLastError ());
        //                    */

        //                    /* Attempt server connection (blocking) */
        //                    nConnection = connect(clientSocket, (struct sockaddr) &serverAddr, sizeof serverAddr);

        //			if (nConnection< 0) 
        //			{
        //				if (bSendError1)
        //				{
        //					pCommsComp->m_nErrorCode = pCommsComp->GetWSAError(WSAGetLastError ());

        //                    sprintf(pCommsComp->m_szTrace, "VCS Failed To Connect (WSAErrorCode %d)\n", pCommsComp->m_nErrorCode);
        //        Console.WriteLine("");
        //        pCommsComp->SendLoggerMessage(T_AMS_IFM, -2, "Failed to connect to VCS.  Re-attempting Connection...");

        //					if (!pCommsComp->m_bShutdownRequested)
        //						pCommsComp->m_tVcsIfManage.bVcsControlPcStatus = true;

        //					for (i=0; i<pCommsComp->m_sTotalExerciseCount; ++i)
        //					{
        //						pCommsComp->ClearVcsExercise(i);
        //        pCommsComp->UpdateExerciseStatus(0, CScenarioComponent::NON_OPERATIONAL, "No_Transmit", 0, n + 700 + 1);
        //    }

        //					for (i=700 + 1; i<=700 + MAX_NUM_NODES; ++i)
        //						pCommsComp->UpdateExerciseStatus(0, CScenarioComponent::NON_OPERATIONAL, "", 0, i);

        //    bSendError1 = false;
        //				}

        //                Sleep(1000);
        //			}
        //			else
        //			{
        //				/* Create thread to handle incoming messages from VCS */
        //				pCommsComp->m_tVcsIfManage.eVCS_LinkState = BUSY_CONNECT;

        //                _beginthread(HandleVCSOutput, 0, pOwner);					
        //			}
        //		}
        //		else
        //		{
        //			if (bSendError2)
        //			{
        //				pCommsComp->m_nErrorCode = pCommsComp->GetWSAError(WSAGetLastError ());

        //                sprintf(pCommsComp->m_szTrace, "VCS Failed To Connect (WSAErrorCode %d)\n", pCommsComp->m_nErrorCode);
        //Console.WriteLine("");
        //pCommsComp->SendLoggerMessage(T_AMS_IFM, -2, "Failed to connect to VCS");

        //				if (!pCommsComp->m_bShutdownRequested)
        //					pCommsComp->m_tVcsIfManage.bVcsControlPcStatus = true;

        //				for (i=0; i<pCommsComp->m_sTotalExerciseCount; ++i)
        //				{
        //					pCommsComp->ClearVcsExercise(i);
        //pCommsComp->UpdateExerciseStatus(0, CScenarioComponent::NON_OPERATIONAL, "No_Transmit", 0, n + 700 + 1);
        //				}

        //				for (i=700 + 1; i<=700 + MAX_NUM_NODES; ++i)
        //					pCommsComp->UpdateExerciseStatus(0, CScenarioComponent::NON_OPERATIONAL, "", 0, i);

        //bSendError2 = false;
        //			}

        //            Sleep(1000);
        //		}
        //	}

        //	/* First message must be a status request */
        //	pCommsComp->SendStatusRequest();

        //Console.WriteLine("VCS Connected... Waiting for Download & Record/Replay PCs\n");


        //	return;
        //}

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : DisconnectFromVCS
        // Description: Disconnects from VCS by closing client socket
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F59301C6
        public UNET_Classes.Enums.eVcsLinkStatus_t DisconnectFromVCS()// eVcsLinkStatus_t DisconnectFromVCS()
        {
            int nStatus;

            /* Close socket*/
            //if (nStatus = shutdown(clientSocket, SD_SEND))				//Never returns
            //		pCommsComp->m_nErrorCode = GetWSAError(WSAGetLastError ());

            //if (nStatus = closesocket(clientSocket))
            //    m_nErrorCode = GetWSAError(WSAGetLastError());

            //WSACleanup();
            //Console.Write("VCS Disconnected\n");



            return UNET_Classes.Enums.eVcsLinkStatus_t.FAILED_CONNECT;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : FindMsgToVCS
        // Description: Finds a queued message awaiting transmission to VCS.  Uses a round
        //							robin approach to ensure no individual exercise hogs interface
        //							although this is unlikely to happen.
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F59301E5
        public int FindMsgToVCS()
        {

            int counter = 0;

            /* Check if currently sending an exercise definition 
                 Do not send any messages from other exercise until ED phase complete
                 as no VCS exercise id is supplied with associated status messages */

            /* Check for non-exercise related messages */
            //if (m_tVcsGenManage.pMsgQueue)
            //{
            //    if (MESSAGE_TRANSMITTED == m_tVcsGenManage.eMsgStatus)
            //        return -2;
            //    else if (!m_tVcsGenManage.bStopAllExerciseProcessing)
            //        return -1;
            //}

            //while (counter < m_sTotalExerciseCount)
            //{
            //    /* Check for remaining messages in current exercise */
            //    if (!m_pVcsExManage[Search_ExNo].bStopAllExerciseProcessing)
            //    {
            //        if (MESSAGE_TRANSMITTED == m_pVcsExManage[Search_ExNo].eMsgStatus)
            //            return -2;

            //        /* stop exercise */
            //        if ((0 != Search_ExNo) &&
            //                (m_pVcsExManage[Search_ExNo].eED_State == ED_DELETE) &&
            //                (m_pVcsExManage[Search_ExNo].eEC_State != EC_STOP))

            //        {
            //            /*	This means there were messages on queue other than stop & delete
            //                    therefore delete queue and ensure stop/delete messages are sent 

            //                    However, need to send logout messages in IL mode otherwise status
            //                    is not updated correctly																					*/

            //            ClearMsgQueue(Search_ExNo);

            //            if (DeleteVcsExercise(Search_ExNo, WITH_STOP))
            //                UpdateExerciseStatus(Search_ExNo, CScenarioComponent::FAULT);  //and continue search
            //            else
            //                return Search_ExNo;
            //        }

            //        /* find exercise definition waiting or in transit */
            //        if (m_pVcsExManage[Search_ExNo].eED_State == ED_WAITING)
            //        {
            //            //if ((m_pVcsExManage[Search_ExNo].eMsgStatus == MESSAGE_READY) &&
            //            //	  (m_pVcsExManage[Search_ExNo].pMsgQueue->MsgHeader.sReserved != 1))
            //            if (m_pVcsExManage[Search_ExNo].eMsgStatus == MESSAGE_READY)
            //                return Search_ExNo;
            //        }

            //        /* find pending exercise control message */
            //        if ((m_pVcsExManage[Search_ExNo].eED_State == ED_LOADED) &&
            //                ((m_pVcsExManage[Search_ExNo].eEC_State == EC_ALLOC_PENDING_LOAD) ||
            //                 (m_pVcsExManage[Search_ExNo].eEC_State == EC_START_PENDING_ALLOC)))
            //        {
            //            if (AllocVcsExercise(Search_ExNo))
            //                UpdateExerciseStatus(Search_ExNo, CScenarioComponent::FAULT);   //and continiue search
            //            else
            //            {
            //                if (m_pVcsExManage[Search_ExNo].eED_State != ED_DELETE)
            //                    m_pVcsExManage[Search_ExNo].eED_State = ED_ALLOC;

            //                return Search_ExNo;
            //            }
            //        }

            //        if ((m_pVcsExManage[Search_ExNo].eED_State == ED_READY) &&
            //                (m_pVcsExManage[Search_ExNo].eEC_State == EC_START_PENDING_ALLOC))
            //        {
            //            if (StartVcsExercise(Search_ExNo))
            //                UpdateExerciseStatus(Search_ExNo, CScenarioComponent::FAULT);   //and continue search
            //            else
            //            {
            //                if (Search_ExNo != 0)
            //                    UpdateExerciseStatus(Search_ExNo, CScenarioComponent::STARTING);

            //                return Search_ExNo;
            //            }
            //        }

            //        /* Check for any other messages */
            //        //if ((m_pVcsExManage[Search_ExNo].eMsgStatus == MESSAGE_READY) &&	 
            //        //		(m_pVcsExManage[Search_ExNo].pMsgQueue->MsgHeader.sReserved != 1))
            //        if (m_pVcsExManage[Search_ExNo].eMsgStatus == MESSAGE_READY)
            //            return Search_ExNo;
            //    }

            //    Search_ExNo = (Search_ExNo == m_sTotalExerciseCount - 1) ? 0 : Search_ExNo + 1;
            //    counter++;
            //}

            //m_tVcsIfManage.lMessageIndex = 0;

            return -2;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : SendMsgToVCS
        // Description: Sends a queued message to the VCS via client socket
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F593020D
        public void SendMsgToVCS(int VIC_ExNo, string pQMsg, int nThreadId)
        {

            int nTxdBytes;

            /* Ignore request to send message when given up with exercise */
            //            if ((VIC_ExNo != -1) && (m_pVcsExManage[VIC_ExNo].bStopAllExerciseProcessing))
            //                return;

            //            /* Verify that not about to corrupt active message handling scheme */
            //            switch (nThreadId)
            //            {
            //                case eTHREAD_EXM:
            //                    if (m_tVcsIfManage.eVCS_LinkState != READY_CONNECT)
            //                    {
            //                        SendLoggerMessage(T_AMS_IFM, VIC_ExNo, "Bad attempt to send data on network #1");
            //                        return;
            //                    }
            //                    Search_ExNo = VIC_ExNo;
            //                    m_tVcsIfManage.eVCS_LinkState = BUSY_CONNECT;
            //                    break;

            //                case eTHREAD_IFM:
            //                    /* Connect state should normally be BUSY_CONNECT but can be BUSY_CONNECT when replying to VCS initiated messages */
            //                    if ((m_tVcsIfManage.eVCS_LinkState != BUSY_CONNECT) &&
            //                            (m_tVcsIfManage.eVCS_LinkState != READY_CONNECT))
            //                    {
            //                        SendLoggerMessage(T_AMS_IFM, VIC_ExNo, "Bad attempt to send data on network #2");
            //                        return;
            //                    }
            //                    break;
            //            }

            //            /* Verify that exercise has message to send */
            //            if (pQMsg == NULL)
            //            {
            //                //SendLoggerMessage(T_AMS_IFM, VIC_ExNo, "Message transmission to VCS requested but empty message queue");
            //                m_tVcsIfManage.eVCS_LinkState = READY_CONNECT;
            //                return;
            //            }

            //            if (VIC_ExNo == -1)
            //            {
            //                if (m_tVcsGenManage.eMsgStatus < MESSAGE_READY)
            //                {
            //                    //SendLoggerMessage(T_AMS_IFM, -1, "Message transmission to VCS requested but no message ready to send");
            //                    m_tVcsIfManage.eVCS_LinkState = READY_CONNECT;
            //                    return;
            //                }

            //                m_tVcsIfManage.nTransmittedVICExerciseNo = -1;
            //            }
            //            else
            //            {
            //                if (m_pVcsExManage[VIC_ExNo].eMsgStatus < MESSAGE_READY)
            //                {
            //                    SendLoggerMessage(T_AMS_IFM, VIC_ExNo, "Message transmission to VCS requested but no message ready to send");
            //                    m_tVcsIfManage.eVCS_LinkState = READY_CONNECT;
            //                    return;
            //                }

            //                m_tVcsIfManage.nTransmittedVICExerciseNo = VIC_ExNo;
            //            }

            //            /* Send message */
            //            if (WaitForSingleObject(m_tVcsIfManage.hTransmitMutex, 2000) == WAIT_TIMEOUT)
            //            {
            //                UpdateExerciseStatus(VIC_ExNo, CScenarioComponent::FAULT, "WAIT_TIMEOUT #1");
            //                return;
            //            }

            //#if _VCS_INTERCHANGE_TRACE
            //		sprintf(m_szTrace, GetVCSError(pQMsg->MsgHeader.sID, VIC_ExNo, eTX));
            //		Console.WriteLine("");
            //#else
            //            /* Detect Reset */
            //            if (MsgHeader.sID == UNET_Classes.Enums.SIM_StatusCodes.I_SC_RESET)
            //                Console.WriteLine("VIC initiated VCS reset\n");
            //#endif

            //#if _VCS_RAW_INTERCHANGE
            //		TRACE("Message Sent %d\n", pQMsg->MsgHeader.sID);
            //#endif

            //            nTxdBytes = send(clientSocket, (char*)&pQMsg->MsgHeader, pQMsg->nMsgSize, 0);

            //            /* Check for successful send */
            //            if (nTxdBytes < 0)
            //            {
            //                m_nErrorCode = GetWSAError(WSAGetLastError());
            //                SendLoggerMessage(T_AMS_IFM, VIC_ExNo, "Failure to send message to VCS");
            //                ReleaseMutex(m_tVcsIfManage.hTransmitMutex);
            //                return;
            //            }

            //            ReleaseMutex(m_tVcsIfManage.hTransmitMutex);

            //            /* Record message send */
            //            m_tVcsIfManage.nTotalMessagesTxThisIteration++;
            //            m_tVcsIfManage.lMessageIndex++;

            //            if (VIC_ExNo == -1)
            //            {
            //                if (m_tVcsGenManage.eMsgStatus == MESSAGE_READY)
            //                    m_tVcsGenManage.eMsgStatus = MESSAGE_TRANSMITTED;
            //                else if (m_tVcsGenManage.eMsgStatus == MESSAGE_TRANSMITTED)
            //                    m_tVcsGenManage.eMsgStatus = MESSAGE_RETRANSMITTED;
            //                else
            //                    SendLoggerMessage(T_AMS_IFM, -1, "Unknown message control status");

            //                /* Remove message from queue if no reply expected */
            //                if (m_tVcsGenManage.pMsgQueue->sMsgExpectedReply == A_NONE)
            //                {
            //                    RemoveQueuedMessage(this, -1);
            //                    SendNextQueuedMessage();
            //                }
            //            }
            //            else
            //            {
            //                if (m_pVcsExManage[VIC_ExNo].eMsgStatus == MESSAGE_READY)
            //                    m_pVcsExManage[VIC_ExNo].eMsgStatus = MESSAGE_TRANSMITTED;
            //                else if (m_pVcsExManage[VIC_ExNo].eMsgStatus == MESSAGE_TRANSMITTED)
            //                    m_pVcsExManage[VIC_ExNo].eMsgStatus = MESSAGE_RETRANSMITTED;
            //                else
            //                    SendLoggerMessage(T_AMS_IFM, VIC_ExNo, "Unknown message control status");

            //                /* Remove message from queue if no reply expected */
            //                if (m_pVcsExManage[VIC_ExNo].pMsgQueue->sMsgExpectedReply == A_NONE)
            //                {
            //                    RemoveQueuedMessage(this, VIC_ExNo);
            //                    SendNextQueuedMessage();
            //                }
            //            }

            return;
        }



        ////////////////////////////////////////////////////////////////////////////////
        // Function   : HandleVCSOutput
        // Description: Thread which handles messages transmitted by the VCS.  Uses blocking
        //							recv function 
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F59302AD
        public void HandleVCSOutput(object pOwner)
        {
            int nBytesRead, nRemainingbytes, nRqdSize, nRxdBytes = 0;
            int VIC_ExNo;
            //char szErrorMsg[50];

            //            pCommsComp = reinterpret_cast<CCommsComponent*>(pOwner);
            //            ASSERT(pCommsComp);

            //            vcsHeader_t pMessageHdr = (vcsHeader_t*)malloc(sizeof(vcsHeader_t));
            //            char pDataSegment = (char)malloc(RX_DATA_SEGMENT_SIZE);

            //            while ((pCommsComp->m_tVcsIfManage.eVCS_LinkState == READY_CONNECT) || ((pCommsComp->m_tVcsIfManage.eVCS_LinkState == BUSY_CONNECT)))
            //            {
            //                nRqdSize = sizeof(vcsHeader_t);
            //                nRxdBytes = 0;

            //                while (nRxdBytes != nRqdSize)
            //                {
            //                    /* Read message header */
            //                    nRemainingbytes = nRqdSize - nRxdBytes;
            //                    nBytesRead = recv(clientSocket, (char*)pMessageHdr + nRqdSize - nRemainingbytes, nRemainingbytes, 0);
            //                    if (nBytesRead < 0)
            //                    {   //connection broken
            //                        pCommsComp->m_nErrorCode = pCommsComp->GetWSAError(WSAGetLastError());
            //                        pCommsComp->DisconnectFromVCS();
            //                        pCommsComp->m_tVcsIfManage.eVCS_LinkState = LOST_CONNECT;
            //                        return;
            //                    }
            //                    nRxdBytes += nBytesRead;
            //                }

            //                /* Check header content (ntohs conversion not required!)*/
            //#if _VCS_RAW_INTERCHANGE
            //			TRACE("Message rxd %d size %d\n",pMessageHdr->sID, pMessageHdr->usLength);
            //#endif

            //                if (pMessageHdr->usLength > (RX_DATA_SEGMENT_SIZE + sizeof(vcsHeader_t)))
            //                {
            //                    pCommsComp->DisconnectFromVCS();
            //                    pCommsComp->m_tVcsIfManage.eVCS_LinkState = LOSTSYNC_CONNECT;
            //                }

            //                nRqdSize = (int)pMessageHdr->usLength;
            //                nRxdBytes = 0;

            //                /* Read message data */
            //                while (nRxdBytes != nRqdSize)
            //                {
            //                    nRemainingbytes = nRqdSize - nRxdBytes;
            //                    nBytesRead = recv(clientSocket, pDataSegment + nRqdSize - nRemainingbytes, nRemainingbytes, 0);
            //                    if (nBytesRead < 0)
            //                    {   //connection broken
            //                        pCommsComp->m_nErrorCode = pCommsComp->GetWSAError(WSAGetLastError());
            //                        pCommsComp->DisconnectFromVCS();
            //                        pCommsComp->m_tVcsIfManage.eVCS_LinkState = LOST_CONNECT;
            //                        return;
            //                    }
            //                    nRxdBytes += nBytesRead;
            //                }

            //#if _VCS_INTERCHANGE_TRACE
            //			if ((pMessageHdr->sID != A_SC_RU_ALIVE) && (pMessageHdr->sID != A_TIME_DATE))
            //			{
            //				sprintf(pCommsComp->m_szTrace, pCommsComp->GetVCSError(pMessageHdr->sID, 0, eRX));
            //				Console.WriteLine("");
            //			}
            //#endif

            //#if _VCS_SYNC_STATUS_TRACE
            //			if ((pMessageHdr->sID == A_SC_RU_ALIVE) || (pMessageHdr->sID == A_TIME_DATE))
            //			{
            //				sprintf(m_szTrace, pCommsComp->GetVCSError(pMessageHdr->sID, 0, eRX));
            //				Console.WriteLine("");
            //			}
            //#endif

            //                if (pCommsComp->UnknownMessage(pMessageHdr->sID))
            //                {
            //                    sprintf(szErrorMsg, "Unknown message received from VCS %d, size %d", pMessageHdr->sID, pMessageHdr->usLength);
            //                    pCommsComp->SendLoggerMessage(T_AMS_IFM, VIC_ExNo, szErrorMsg);
            //                    pCommsComp->SendNextQueuedMessage();
            //                }

            //                /* Interpret input message */
            //                else if ((VIC_ExNo = pCommsComp->ProcessReceivedMessage(pMessageHdr->sID, pDataSegment, pMessageHdr->usLength)) != -2)
            //                {
            //                    /* Correct message reply received - (i.e. no retransmit of previous message) */
            //                    RemoveQueuedMessage(pCommsComp, VIC_ExNo);
            //                    pCommsComp->SendNextQueuedMessage();
            //                }
            //            }

            //            /* Connection lost - tidy up (non-network) */
            //            free(pMessageHdr);
            //            free(pDataSegment);

        }

        ///////////////////////////////////////////////////////////////////////////////
        // Function   : RemoveQueuedMessage
        // Description: Removes transmitted message from appropriate queue
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F593037F
        public void RemoveQueuedMessage(object pOwner, int VIC_ExNo)
        {
            //            QueuedMsg_t pNextQueuedMsg;

            //            pCommsComp = reinterpret_cast<CCommsComponent*>(pOwner);
            //            ASSERT(pCommsComp);

            //            if (VIC_ExNo == -1)
            //            {
            //                if (pCommsComp->m_tVcsGenManage.nMsgCnt == 0)
            //                {
            //#if _VIC_CMND_STATUS_TRACE
            //				Console.WriteLine("VCS (IL) Cannot remove queued item as queue size is zero\n");	
            //#endif
            //                }
            //                else
            //                {
            //                    /* Wait for unbusy exercise structure then remove associated output message */
            //                    if (WaitForSingleObject(pCommsComp->m_tVcsGenManage.hQueueMutex, 2000) == WAIT_TIMEOUT)
            //                    {
            //                        pCommsComp->UpdateExerciseStatus(-1, CScenarioComponent::FAULT, "WAIT_TIMEOUT #2");
            //                        return;
            //                    }

            //                    /* Remove queued message */
            //                    pNextQueuedMsg = pCommsComp->m_tVcsGenManage.pMsgQueue->pNextMsg;
            //                    free(pCommsComp->m_tVcsGenManage.pMsgQueue);
            //                    pCommsComp->m_tVcsGenManage.pMsgQueue = pNextQueuedMsg;
            //                    if (--pCommsComp->m_tVcsGenManage.nMsgCnt <= 0)
            //                    {
            //                        pCommsComp->m_tVcsGenManage.eMsgStatus = MESSAGE_IDLE;
            //                        if (pCommsComp->m_tVcsGenManage.nMsgCnt < 0)
            //                        {
            //                            pCommsComp->m_tVcsGenManage.nMsgCnt = 0;
            //                            pCommsComp->SendLoggerMessage(T_AMS_IFM, -1, "Bad message free");
            //                        }
            //                    }
            //                    else
            //                        pCommsComp->m_tVcsGenManage.eMsgStatus = MESSAGE_READY;

            //                    ReleaseMutex(pCommsComp->m_tVcsGenManage.hQueueMutex);
            //                }
            //            }
            //            else
            //            {
            //                if (pCommsComp->m_pVcsExManage[VIC_ExNo].nMsgCnt == 0)
            //                {
            //#if _VIC_CMND_STATUS_TRACE
            //				sprintf(pCommsComp->m_szTrace, "VCS (Ex %d) Cannot remove queued item as queue size is zero\n", VIC_ExNo);
            //				Console.WriteLine("");
            //#endif
            //                }
            //                else
            //                {
            //                    /* Wait for unbusy exercise structure then remove associated output message */
            //                    if (WaitForSingleObject(pCommsComp->m_pVcsExManage[VIC_ExNo].hQueueMutex, 2000) == WAIT_TIMEOUT)
            //                    {
            //                        pCommsComp->UpdateExerciseStatus(VIC_ExNo, CScenarioComponent::FAULT, "WAIT_TIMEOUT #3");
            //                        return;
            //                    }

            //                    /* Remove queued message */
            //                    pNextQueuedMsg = pCommsComp->m_pVcsExManage[VIC_ExNo].pMsgQueue->pNextMsg;
            //                    free(pCommsComp->m_pVcsExManage[VIC_ExNo].pMsgQueue);
            //                    pCommsComp->m_pVcsExManage[VIC_ExNo].pMsgQueue = pNextQueuedMsg;
            //                    pCommsComp->m_pVcsExManage[VIC_ExNo].nMsgCnt--;
            //                    if (pCommsComp->m_pVcsExManage[VIC_ExNo].nMsgCnt == 0)
            //                        pCommsComp->m_pVcsExManage[VIC_ExNo].eMsgStatus = MESSAGE_IDLE;
            //                    else
            //                        pCommsComp->m_pVcsExManage[VIC_ExNo].eMsgStatus = MESSAGE_READY;

            //                    ReleaseMutex(pCommsComp->m_pVcsExManage[VIC_ExNo].hQueueMutex);
            //                }
            //            }

            //            /* Update interface control structure to show no output message */
            //            pCommsComp->m_tVcsIfManage.nTransmittedVICExerciseNo = -2;
            //            //pCommsComp->m_tVcsIfManage.lMessageIndex = 0;

            return;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : UnknownMessage
        // Description: Returns 'true' if received message id is unexpected 
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F59302E9
        public bool UnknownMessage(short rxdMessageId)
        {
            bool bUnknownMsg;

            switch (rxdMessageId)
            {
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_LOAD:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_START:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_STOP:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PAUSE:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PLAYBACK:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_SEEK:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ERROR_REPORT:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_CONFIG:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_DEL:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_END:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RP_CONFIG:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_START:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RU_ALIVE:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_TIME_DATE:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_INVALID_MSG:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGIN:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGOUT:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_ROLE_DEF:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_MONITOR_START:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_MONITOR_END:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_IMPOSE_START:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_IMPOSE_END:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_REPLAY:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RESUME:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_ALLOC:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RESET:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_SHUTDOWN:
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_POWER_OFF:

                    bUnknownMsg = false;
                    break;

                default:
                    /* 
                    Unused replies:-	A_GET_EX_BY_ID:		
                                                        A_GET_EX_BY_NAME:
                                                        A_EC_EX_STATE:
                    */

                    bUnknownMsg = true;
                    break;
            }
            return bUnknownMsg;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : ProcessReceivedMessage
        // Description: Performs the necessary actions in response to a message from VCS
        //							If a message is to be removed from an exercise queue this member 
        //							returns an exercise number else it returns -2. 
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F5930325
        public int ProcessReceivedMessage(short rxdMessageId, char pDataSegment, short sDataSize)
        {
            //bool bUnknownMsg;
            ushort pusStatus;
            ushort usStatus;
            ushort usVcsExerciseNo;

            bool bGoodVcsControlStatus = false;
            bool bGoodVcsNodeStatus = false;

            short sAMSExerciseNo;

            //int nQueueRemovalEnable = 1;
            int n, i, offset;
            int nRoleIdx;
            int nExRef;

            //   char szErrorString[128];
            //   char szTemp1[100];
            //   char szTemp2[100];

            // //  msgVCSMonitor_t pMonPDU;
            ////   Login_t pLoginReply;

            //   bool bFoundTrainNode, bFoundInstrNode;
            //   bool bKnownUnsolicitedMessage = false;

            //   pusStatus = (ushort)pDataSegment;


            //   /* Check for reply to synchronisation message which is never queued */
            //   if (rxdMessageId == (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_TIME_DATE)
            //   {
            //       usStatus = pusStatus;

            //       if (usStatus != VCS_SUCCESS)    //INV_DATE, INV_TIME
            //           SendLoggerMessage(T_VCS_FAIL, -2, "VCS reported failure to synchronise");

            //       return -2;
            //   }

            //   /* Check for reply to status request message which is never queued */
            //   if (rxdMessageId == (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RU_ALIVE)
            //   {
            //       m_tVcsIfManage.msgRU_ALIVE.MsgData[WD_RUALIVE_NOREPLY] = 0;
            //       m_bInhibitStatusResponseError = false;

            //       if (DetectStatusChange(pDataSegment, &bGoodVcsControlStatus, &bGoodVcsNodeStatus))
            //       {
            //           /* Send Message to Sys Admin */
            //           Console.WriteLine("VCS Status Change\n");
            //       }

            //       /* If node has powerered up then logon appropropriate roles */
            //       UpdateNodeStatus();

            //       /* Enable transfer of reset message and other exercise data */
            //       if (m_bVCS_Startup &&
            //                   (m_tSystemStatus.cSystemPC == 0x03) &&
            //                   (m_tSystemStatus.cDownloadPC == 0x01) &&
            //                   (m_tSystemStatus.cRecordReplayPC & 0x1))
            //       {
            //           m_bVCS_Startup = false;

            //           if (bGoodVcsControlStatus)
            //               Console.WriteLine("VCS Download & Record/Replay PCs Ready (Archive PC available)\n");
            //           else
            //               Console.WriteLine("VCS Download & Record/Replay PCs Ready (Archive PC not available)\n");

            //           SendNextQueuedMessage();
            //       }
            //       else if (m_bVCS_Startup &&
            //                   (m_tSystemStatus.cSystemPC == 0x3) &&
            //                   (m_tSystemStatus.cDownloadPC == 0x1))
            //       {
            //           Console.WriteLine("Waiting for Record/Replay PC\n");
            //       }


            //       /* If all VCS control PCs ok then send good status else send bad Comms PC status */
            //       if (bGoodVcsControlStatus && !m_bVCS_Startup)
            //           m_tVcsIfManage.bVcsControlPcStatus = false;
            //       else
            //           m_tVcsIfManage.bVcsControlPcStatus = true;

            //       //m_pSystem->SetSystemState(m_tVcsIfManage.bVcsControlPcStatus, 0, 0);

            //       /* Check individual node status and send status message to Session manager if required */
            //       for (n = 0; n <= m_sTotalNumRoles; n++)
            //       {
            //           /* get session manager status node index */
            //           if (n == m_sTotalNumRoles)
            //               offset = n;                                                         //Debrief
            //           else if (n > m_sTotalNumTrainees - 1)
            //               offset = n - m_sTotalNumTrainees;               //Instructor
            //           else
            //               offset = n + m_sTotalNumInstructors;        //Trainee		

            //           if (m_tSystemStatus.cCommsNode[n] & 0x1)
            //           {
            //               /* Use RUNNING_BACKWARDS to run specific code setting UNINITIALISED within UpdateExerciseStatus function */
            //               if ((BYTE)CScenarioComponent::NON_OPERATIONAL == m_tVcsIfManage.cVcsNodeStatus[offset])
            //                   UpdateExerciseStatus(0, CScenarioComponent::RUNNING_BACKWARDS, "No_Transmit", 0, n + 700 + 1);
            //           }
            //           else
            //               UpdateExerciseStatus(0, CScenarioComponent::NON_OPERATIONAL, "No_Transmit", 0, n + 700 + 1);
            //       }

            //       UpdateExerciseStatus(0, 0, "No_Status_Update");

            //       return -2;
            //   }

            //   /* Check for reply to shutdown message which is never queued */
            //   if (rxdMessageId == (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_SHUTDOWN)
            //   {
            //       usStatus = pusStatus;

            //       if (usStatus != VCS_SUCCESS)    //FAILURE
            //           SendLoggerMessage(T_VCS_FAIL, -2, "VCS reported failure to shutdown");
            //       else
            //           Console.WriteLine("VCS Shutdown Request Acknowledged\n");

            //       return -2;
            //   }

            //   if (rxdMessageId == (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_POWER_OFF)
            //   {

            //       ClearVcsExercise(-2);

            //       for (i = 0; i < m_sTotalExerciseCount; ++i)
            //       {
            //           ClearVcsExercise(i);
            //           UpdateExerciseStatus(0, CScenarioComponent::NON_OPERATIONAL, "No_Transmit", 0, i + 700 + 1);
            //       }

            //       for (i = 700 + 1; i <= 700 + MAX_NUM_NODES; ++i)
            //           UpdateExerciseStatus(0, CScenarioComponent::POWERED_ON, "", 0, i);

            //       SendLoggerMessage(T_VCS_FAIL, -2, "VCS reported shutdown complete");
            //       Console.WriteLine("VCS Shutdown Complete\n");
            //       return -2;
            //   }

            //   /* Check for unsolicited incoming message */
            //   if ((rxdMessageId == I_ERROR_REPORT) ||
            //           (rxdMessageId == I_MONITOR_START) ||
            //           (rxdMessageId == I_MONITOR_END) ||
            //           (rxdMessageId == A_EC_SEEK))
            //       bKnownUnsolicitedMessage = true;

            //   /* If not unsolicited incoming message then verify this is an expected reply */
            //   if (!bKnownUnsolicitedMessage &&
            //           (m_tVcsIfManage.nTransmittedVICExerciseNo != -2))

            //   {   /* Should have received a reply associated with transmitted message - verify reply is as expected */
            //       if (m_tVcsIfManage.nTransmittedVICExerciseNo == -1)
            //       {
            //           if (m_tVcsGenManage.pMsgQueue == NULL)
            //           {
            //               strcpy(szTemp1, GetVCSError(rxdMessageId, AMS_NO_ERROR, eERROR));
            //               sprintf(m_szVCSError, "Unexpected msg rxd - no reply expected (EXERCISE: -1, Msg: %s)",
            //                   szTemp1);

            //               SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, m_szVCSError);
            //               return -2;
            //           }
            //           else if (rxdMessageId != m_tVcsGenManage.pMsgQueue->sMsgExpectedReply)
            //           {
            //               strcpy(szTemp1, GetVCSError(rxdMessageId, AMS_NO_ERROR, eNAME));
            //               strcpy(szTemp2, GetVCSError(m_tVcsGenManage.pMsgQueue->sMsgExpectedReply, AMS_NO_ERROR, eNAME));
            //               sprintf(m_szVCSError, "Unexpected msg rxd - reply %s expected (EXERCISE: -1 Msg: %s)",
            //                   szTemp1,
            //                   szTemp2);

            //               SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, m_szVCSError);
            //               return -1;
            //           }

            //       }
            //       else
            //       {
            //           if (m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].pMsgQueue == NULL)
            //           {
            //               strcpy(szTemp1, GetVCSError(rxdMessageId, AMS_NO_ERROR, eERROR));
            //               sprintf(m_szVCSError, "Unexpected msg rxd - no reply expected (EXERCISE: %d, Msg: %s)",
            //                   m_tVcsIfManage.nTransmittedVICExerciseNo,
            //                   szTemp1);

            //               SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, m_szVCSError);
            //               return -2;
            //           }
            //           else if (rxdMessageId != m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].pMsgQueue->sMsgExpectedReply)
            //           {
            //               strcpy(szTemp1, GetVCSError(rxdMessageId, AMS_NO_ERROR, eERROR));
            //               strcpy(szTemp2, GetVCSError(m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].pMsgQueue->sMsgExpectedReply, AMS_NO_ERROR, eERROR));
            //               sprintf(m_szVCSError, "Unexpected msg rxd -  reply %s expected (EXERCISE: %d Msg: %s)",
            //                   szTemp1,
            //                   m_tVcsIfManage.nTransmittedVICExerciseNo,
            //                   szTemp2);

            //               SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, m_szVCSError);
            //               return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //           }
            //       }
            return 0;
            //  }

            /* Prevent recognised (but unsolicited) messages from crashing system */
            //if (m_tVcsIfManage.nTransmittedVICExerciseNo == -2)
            //	nQueueRemovalEnable= -1;

            //            switch (rxdMessageId)
            //            {
            //                /************ Exercise Definition *************/

            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_START:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_CONFIG:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_ROLE_DEF:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_END:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_DEL:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RESET:

            //                    usStatus = pusStatus;                              //ntohs(*(ushort*) pDataSegment);

            //                    switch (usStatus)
            //                    {
            //                        case VCS_SUCCESS:

            //                            switch (rxdMessageId)
            //                            {
            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_DEL:
            //                                    m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].eEC_State = EC_NULL;

            //                                    /* Check if there is an exercise waiting to be loaded */
            //                                    if (REPLAY_IDX == m_tVcsIfManage.nTransmittedVICExerciseNo)
            //                                        nExRef = m_sAmsReplayExerciseNumber;
            //                                    else
            //                                        nExRef = m_tVcsIfManage.nTransmittedVICExerciseNo;

            //                                    ClearVcsExercise(m_tVcsIfManage.nTransmittedVICExerciseNo); //also frees unsent messages and sends status message

            //                                    if ((nExRef > 0) &&
            //                                            (NULL != m_SttExSpecification[nExRef]))
            //                                    {
            //                                        ProcessMessage((void*)m_SttExSpecification[nExRef], sizeof(msgVCSCommunicationSTTConfirmation_t));
            //                                        delete m_SttExSpecification[nExRef];
            //                                        m_SttExSpecification[nExRef] = NULL;
            //                                    }

            //                                    SendNextQueuedMessage();
            //                                    return -2;

            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RESET:
            //                                    Console.WriteLine("VCS Reset Completed\n");

            //                                    /* Initialise exercise & non-exercise management structures */
            //                                    m_bResetComplete = true;

            //                                    if (m_bExternalReset)
            //                                    {
            //                                        m_bExternalReset = false;

            //                                        ClearVcsExercise(-2);

            //                                        for (i = 0; i < m_sTotalExerciseCount; ++i)
            //                                        {
            //                                            ClearVcsExercise(i);
            //                                        }

            //                                        return -2;
            //                                    }
            //                                    break;

            //                                default:
            //                                    break;
            //                            }
            //                            break;

            //                        default:    //A_ES_START		-		FAILURE, INV_EX_NAME, ES_BUSY, UNDEFINED
            //                                    //A_ES_CONFIG		-		FAILURE, INV_STATE, INV_ES_DATA
            //                                    //A_ES_ROLE_DEF -		FAILURE
            //                                    //A_ES_END			-		FAILURE, INV_STATE
            //                                    //A_ES_DEL			-		FAILURE, INV_EX_NAME, ES_BUSY
            //                                    //A_SC_RESET		-		FAILURE

            //                            //Note ES_BUSY means that exercise specification is in use therefore cannot be deleted or 
            //                            //exercise has started and cannot be restarted.
            //                            switch (rxdMessageId)
            //                            {
            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_DEL:
            //                                    if (usStatus == VCS_INV_EX_NAME)    //Exercise has already been deleted
            //                                        ClearVcsExercise(m_tVcsIfManage.nTransmittedVICExerciseNo);
            //                                    else
            //                                        UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                                    break;

            //                                default:
            //                                    UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                                    break;
            //                            }
            //                            break;
            //                    }

            //                    return nTransmittedVICExerciseNo;
            //                    break;

            //                /************ Exercise Load *************/

            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_LOAD:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_REPLAY:

            //                    usVcsExerciseNo = pusStatus++;
            //                    usStatus = pusStatus;

            //                    //switch (usStatus)
            //                    //{

            //                    //    case VCS_SUCCESS:

            //                    //        /* Store VCS Exercise ID for use with future messages */
            //                    //        m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].sVcsExerciseNumber = usVcsExerciseNo;

            //                    //        /* Validate messages in queue that do not have correct VCS exercise number */
            //                    //        //ValidateMsgQueue(m_tVcsIfManage.nTransmittedVICExerciseNo);

            //                    //        /* Remove exercise stop flag to enable further message processing (block command handling) */
            //                    //        m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].bStoppingExercise = false;

            //                    //        /* Check load/replay status */
            //                    //        if (rxdMessageId == (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_LOAD)
            //                    //        {
            //                    //            /* Check short-term recording request status */
            //                    //            if ((ushort)(pDataSegment + 4) != m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].sShortTermRecordControl * 2)
            //                    //                SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, "Incorrect short-term recording request response");

            //                    //            /* Check long-term recording request status */
            //                    //            if ((ushort)(pDataSegment + 6) != m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].sLongTermRecordControl * 2)
            //                    //                SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, "Incorrect long-term recording request response");

            //                    //            if (strlen(m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].szRecordFileName))
            //                    //            {
            //                    //                memset(szErrorString, 0, sizeof(szErrorString));
            //                    //                strncpy(szErrorString, (char)(pDataSegment + 8), strlen(m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].szRecordFileName));
            //                    //                if (strcmp(m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].szRecordFileName, szErrorString))
            //                    //                    SendLoggerMessage(T_VCS_FAIL, m_tVcsIfManage.nTransmittedVICExerciseNo, "Problem with recording file-name");
            //                    //            }
            //                    //        }

            //                    //        if (m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].eED_State != ED_DELETE)
            //                    //            m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].eED_State = ED_LOADED;

            //                    //        break;

            //                    //    default:    //A_EC_LOAD		-		FAILURE, INV_EX_NAME, INV_EX_TIME
            //                    //                //A_EC_REPLAY -		FAILURE, INV_EX_NAME, INV_FILE

            //                    //        UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                    //        break;
            //                    //}

            //                    return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //                    break;

            //                /***************** Exercise Control ******************/
            //                /*****************   Record/Replay  ******************/

            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_ALLOC:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_START:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_STOP:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PAUSE:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RESUME:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PLAYBACK:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RP_CONFIG:

            //                    usVcsExerciseNo = pusStatus++;
            //                    usStatus = pusStatus++;

            //                    if ((sAMSExerciseNo = Miscellaneous.FindVcsExercise(usVcsExerciseNo)) != m_tVcsIfManage.nTransmittedVICExerciseNo)
            //                    {
            //                        if (rxdMessageId == (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_STOP)
            //                        {
            //                            sprintf(m_szTrace, "Cannot handle A_EC_STOP, exercise not found - already stopped");
            //                            Console.WriteLine("");
            //                            /*	 What Exerrcise ID to return to get meessage clearedr from List ??	*/
            //                            return -2; //m_tVcsIfManage.nTransmittedVICExerciseNo;

            //                        }
            //                        else
            //                        {
            //                            sprintf(m_szTrace, "Exercise not found Cannot handle %d/n", rxdMessageId);
            //                            Console.WriteLine("");
            //                            UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, AMS_WRONG_EXERCISE, eERROR));
            //                            return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //                        }
            //                    }

            //                    switch (usStatus)
            //                    {
            //                        case VCS_SUCCESS:

            //                            switch (rxdMessageId)
            //                            {
            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_START:
            //                                    m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_START;
            //                                    if (m_pVcsExManage[sAMSExerciseNo].eED_State != ED_DELETE)
            //                                        m_pVcsExManage[sAMSExerciseNo].eED_State = ED_ACTIVE;

            //                                    if (strcmp(m_pVcsExManage[sAMSExerciseNo].szExerciseMode, "IL"))
            //                                        UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::RUNNING);  //Running STT							
            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_STOP:
            //                                    //if (m_pVcsExManage[sAMSExerciseNo].eEC_State == EC_RESTART)
            //                                    //{
            //                                    //    /* May restart exercise - force re-allocation of desks and login again */

            //                                    //    /* Prepare for restart */
            //                                    //    m_pVcsExManage[sAMSExerciseNo].sVcsExerciseNumber = -2;
            //                                    //    m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_ALLOC_PENDING_LOAD;
            //                                    //    m_pVcsExManage[sAMSExerciseNo].eED_State = ED_SPECIFIED;
            //                                    //    LoadVcsExercise(sAMSExerciseNo);
            //                                    //    m_pVcsExManage[sAMSExerciseNo].bRestart = true;

            //                                    //    /* Clear connectivity matrix as full radio connectivity assumed by VCS at reload*/
            //                                    //    for (n = 0; n < m_sTotalNumDesks; ++n)
            //                                    //    {
            //                                    //        m_pVcsExManage[sAMSExerciseNo].RefConnectivity[n].ownPlatformHFMast = eMASK_NONE;
            //                                    //        m_pVcsExManage[sAMSExerciseNo].RefConnectivity[n].ownPlatformUHFMast = eMASK_NONE;
            //                                    //        for (i = 0; i < NO_OF_COMMS_VEHICLES; ++i)
            //                                    //        {
            //                                    //            m_pVcsExManage[sAMSExerciseNo].RefConnectivity[n].friendlyVehicles[i] = -1; //(NOT_RELEVANT = -1)
            //                                    //            m_pVcsExManage[sAMSExerciseNo].RefConnectivity[n].availableUHFComms[i] = eMASK_NONE;
            //                                    //            m_pVcsExManage[sAMSExerciseNo].RefConnectivity[n].availableHFComms[i] = eMASK_NONE;
            //                                    //        }
            //                                    //    }
            //                                   // }
            //                                    else
            //                                    {
            //                                     //   m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_STOP;
            //                                        /* If stopping IL Session then do not update status */
            //                                     //   if (strcmp(m_pVcsExManage[sAMSExerciseNo].szExerciseMode, "IL"))
            //                                     //.......................................................................       UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::STOPPED);
            //                                    }

            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PAUSE:
            //                                    //if (m_pVcsExManage[sAMSExerciseNo].eEC_State != EC_RESTART) // Gerry this is the change to add to build
            //                                    //{
            //                                    //    m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_PAUSE;
            //                                    //    UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FROZEN);
            //                                    //}
            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PLAYBACK:
            //                                    //m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_PLAYBACK;
            //                                    //if (m_pVcsExManage[sAMSExerciseNo].eED_State != ED_DELETE)
            //                                    //    m_pVcsExManage[sAMSExerciseNo].eED_State = ED_ACTIVE;
            //                                    //UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::RUNNING);
            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RESUME:
            //                                    // change load type for future starts 
            //                                    //m_pVcsExManage[sAMSExerciseNo].eLoadType = EXERCISE_NEW;
            //                                    //m_pVcsExManage[sAMSExerciseNo].eED_State = ED_READY;
            //                                    //UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FROZEN);
            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RP_CONFIG:
            //                                    /* This is normally the last messages sent during VCS Exercise specification and load
            //                                         The message is therefore used to flag that VIC is ready to partake in exercise 
            //                                         Exception is if Exercise recovery is taking place in which case a subsequent seek/resume is required

            //                                         NOTE: IL Session returns an invalid exercise to this command			*/

            //                                    if ((m_pVcsExManage[sAMSExerciseNo].eED_State == ED_ALLOC) &&
            //                                            (m_pVcsExManage[sAMSExerciseNo].nMsgCnt == 1))
            //                                    {
            //                                        if (EXERCISE_RECOVERY != m_pVcsExManage[sAMSExerciseNo].eLoadType)
            //                                        {
            //                                            if (m_pVcsExManage[sAMSExerciseNo].eED_State != ED_DELETE)
            //                                            {
            //                                                if (m_pVcsExManage[sAMSExerciseNo].bRestart)
            //                                                {
            //                                                    m_pVcsExManage[sAMSExerciseNo].bRestart = false;
            //                                                    UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::STOPPED);
            //                                                }
            //                                                else
            //                                                    UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::LOADED);

            //                                                m_pVcsExManage[sAMSExerciseNo].eED_State = ED_READY;


            //             }
            //                                        /* Check if there is a load snapshot message waiting to be actioned
            //                                        This will also result in a RESUME message being sent to convert from
            //                                        playback to record exercise type */
            //                                        if ((m_tVcsIfManage.nTransmittedVICExerciseNo > 0) &&
            //                                                (NULL != m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo]))
            //                                        {
            //                                            ProcessMessage((void*)m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo], sizeof(msgVCSCommunicationSTTConfirmation_t));
            //                                            delete m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo];
            //                                            m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo] = NULL;
            //                                        }
            //                                    }
            //                                    break;
            //                            }

            //                            return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //                            break;

            //                        default:    //A_EC_ALLOC		-		FAILURE, INV_EX_NAME, INV_EX_TIME
            //                                    //A_EC_START		-		FAILURE, INV_EX_NAME, INV_STATE
            //                                    //A_EC_STOP 		-		FAILURE, INV_EX_NUM
            //                                    //A_EC_PAUSE 		-		FAILURE, INV_EX_NUM, INV_STATE
            //                                    //A_EC_RESUME:
            //                                    //A_EC_PLAYBACK:
            //                                    //A_EC_RP_CONFIG-		FAILURE, INV_STATE, INV_ES_DATA

            //                            switch (rxdMessageId)
            //                            {
            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RP_CONFIG:
            //                                    /* The recording is disabled this command will result in an invalid exercise message being returned - used to flag session loaded */
            //                                    //if ((m_pVcsExManage[sAMSExerciseNo].eED_State == ED_ALLOC) &&
            //                                    //        (m_pVcsExManage[sAMSExerciseNo].sShortTermRecordControl == 0))
            //                                    //{
            //                                    //    if (m_pVcsExManage[sAMSExerciseNo].eED_State != ED_DELETE)
            //                                    //    {
            //                                    //        if (strcmp(m_pVcsExManage[sAMSExerciseNo].szExerciseMode, "IL"))
            //                                    //        {   /* STT Session */
            //                                    //            if (m_pVcsExManage[sAMSExerciseNo].bRestart)
            //                                    //            {
            //                                    //                m_pVcsExManage[sAMSExerciseNo].bRestart = false;
            //                                    //                UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::STOPPED);
            //                                    //            }
            //                                    //            else
            //                                    //                UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::LOADED);
            //                                    //        }

            //                                    //        m_pVcsExManage[sAMSExerciseNo].eED_State = ED_READY;

            //                                    //        /* Check if there is a load snapshot message waiting to be actioned */
            //                                    //        if (NULL != m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo])
            //                                    //        {
            //                                    //            ProcessMessage((void*)m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo], sizeof(msgVCSCommunicationSTTConfirmation_t));
            //                                    //            delete m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo];
            //                                    //            m_LoadSnapshot[m_tVcsIfManage.nTransmittedVICExerciseNo] = NULL;
            //                                    //        }
            //                                    //    }
            //                                    //    return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //                                    //}
            //                                    //else
            //                                    //{
            //                                    //    sprintf(m_szTrace, "Reply VCS Exercise ID %d\n", usVcsExerciseNo);
            //                                    //    Console.WriteLine("");
            //                                    //    UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                                    //}
            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PAUSE:
            //                                    /* VCS will automatically go into pause state during replay when end of exercise reached
            //                                    //    If another pause message is transmitted this will result in an invalid state reply */
            //                                    //if (usStatus == VCS_INV_STATE)
            //                                    //{
            //                                    //    m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_PAUSE;
            //                                    //    UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FROZEN);
            //                                    //}
            //                                    //else
            //                                    //    UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));

            //                                    break;

            //                                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_STOP:
            //                                    // Failure to stop successfully- report error and prevent new exercise being loaded by forcing EC_STOP
            //                                 //   UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                                  //  m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_STOP;

            //                                    /* problem whan unloading exercise - exercise emust already have been stopped  
            //                                    if ((EC_RESTART != m_pVcsExManage[sAMSExerciseNo].eEC_State) || (VCS_INV_EX_NUM == usStatus))
            //                                    {
            //                                        m_pVcsExManage[sAMSExerciseNo].eEC_State = EC_STOP;
            //                                        UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::STOPPED);
            //                                    }*/

            //                                    break;

            //                                default:
            //                                 //   UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                                    break;

            //                            }
            //                            return UNET_Classes.Enums.m_tVcsIfManage.nTransmittedVICExerciseNo;
            //                            break;
            //                    }
            //                    break;

            //                /****************** Exercise Login *******************/

            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGIN:
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGOUT:

            //                    usVcsExerciseNo = pusStatus++;
            //                    usStatus = pusStatus++;

            //                    if ((sAMSExerciseNo = FindVcsExercise(usVcsExerciseNo)) != m_tVcsIfManage.nTransmittedVICExerciseNo)
            //                    {
            //                        UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, AMS_WRONG_EXERCISE, eERROR));
            //                        return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //                    }

            //                    /* Find role index */
            //                    pusStatus--;
            //                    pLoginReply = (Login_t)pusStatus;      //structure is not a perfect match therefore offset pointer

            //                    for (n = 0; n < m_pVcsExManage[sAMSExerciseNo].sRoleCount; n++)
            //                    {
            //                        for (i = 0; i < sizeof(m_pVcsExManage[sAMSExerciseNo].Roles[n].szRoleName) ; i++)
            //				{
            //                        if (m_pVcsExManage[sAMSExerciseNo].Roles[n].szRoleName[i] != pLoginReply->cRoleName[i])
            //                            break;
            //                    }

            //                    if (i == sizeof(m_pVcsExManage[sAMSExerciseNo].Roles[n].szRoleName) )
            //					break;

            //            }

            //            if (n == m_pVcsExManage[sAMSExerciseNo].sRoleCount)
            //            {
            //                UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, AMS_UNKNOWN_ROLE, eERROR));
            //                return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //            }

            //            nRoleIdx = n;

            //            switch (usStatus)
            //            {
            //                case VCS_SUCCESS:

            //                    switch (rxdMessageId)
            //                    {
            //                        case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGIN:
            //                            m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].eLoginStatus = LOGIN_SUCCESS;
            //                            sprintf(m_szTrace, "VCS LOGIN SUCCESSFUL (Node %d, Exercise %d) \n", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sPhysicalNode, sAMSExerciseNo);
            //                            Console.WriteLine("");

            //                            if (!strcmp(m_pVcsExManage[sAMSExerciseNo].szExerciseMode, "IL"))
            //                                UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::RUNNING_IL, "", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sLogicalNode);

            //                            if ((ED_ALLOC != m_pVcsExManage[sAMSExerciseNo].eED_State) &&
            //                                    (REPLAY_IDX != sAMSExerciseNo) &&
            //                                    (0 != sAMSExerciseNo))
            //                            { /* This must be a login due to node power cycling or IL participation in active IL session */
            //                                UpdateExerciseStatus(sAMSExerciseNo, m_pVcsExManage[sAMSExerciseNo].cScenarioState, "", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sLogicalNode);
            //                            }

            //                            if ((EXERCISE_REPLAY == m_pVcsExManage[sAMSExerciseNo].eLoadType) &&
            //                                    (m_pVcsExManage[Search_ExNo].eED_State > ED_ALLOC))
            //                            {
            //                                UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::TRAINEE_CHANGED);

            //                                sprintf(m_szTrace, "VCS Exercise %d RP CONFIG CHANGED\n", sAMSExerciseNo);
            //                                Console.WriteLine("");
            //                            }
            //                            break;

            //                        case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGOUT:
            //                            m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].eLoginStatus = LOGIN_NOT;
            //                            sprintf(m_szTrace, "VCS LOGOUT SUCCESSFUL (Node %d, Exercise %d) \n", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sPhysicalNode, sAMSExerciseNo);
            //                            Console.WriteLine("");

            //                            /* If logging out of IL Session then update individual node status */
            //                            if (!strcmp(m_pVcsExManage[sAMSExerciseNo].szExerciseMode, "IL"))
            //                                UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::UNINITIALISED, "", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sLogicalNode);

            //                            break;
            //                    }
            //                    break;


            //                    //    default:    //A_LOGIN		-		FAILURE, INV_EX_NUM, INV_ROLE_ID, INV_POS
            //                    //A_LOGOUT	-		FAILURE, INV_EX_NUM, INV_ROLE_ID
            //            }
            //            switch (rxdMessageId)
            //            {
            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGIN:
            //                    if (m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].eLoginStatus == LOGIN_ACTIVE)
            //                    { /* Node must still be initialising - attempt to resend message once (after short delay)			 */
            //                      /* Troy plan to change 'Position OK' message such that only set when initialisation complete */
            //                      /* Will leave this code as is but it should not be necessary																 */
            //                        if (m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].eMsgStatus == MESSAGE_TRANSMITTED)
            //                        {
            //                            System.Threading.Thread.Sleep(2000);
            //                            //eMsgStatus will change to MESSAGE_RETRANSMITTED;
            //                            SendMsgToVCS(m_tVcsIfManage.nTransmittedVICExerciseNo, m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].pMsgQueue, eTHREAD_IFM);
            //                            return -2;
            //                        }
            //                    }

            //                    /* Report error to login */
            //                    m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].eLoginStatus = LOGIN_FAILED;
            //                    if (m_pVcsExManage[sAMSExerciseNo].eLoadType == EXERCISE_REPLAY)
            //                        sprintf(szErrorString, "VCS LOGIN (REPLAY) FAILED (Recorded Node %d) \n", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sPhysicalNode);
            //                    else
            //                        sprintf(szErrorString, "VCS LOGIN FAILED (Node %d) \n", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sPhysicalNode);

            //                    //UpdateExerciseStatus(sAMSExerciseNo, CScenarioComponent::FAULT, szErrorString, m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sLogicalNode);
            //                    SendLoggerMessage(T_VCS_ERR, m_tVcsIfManage.nTransmittedVICExerciseNo, szErrorString);


            //                    /* Force login if desk is reset or comes on line */
            //                    m_tVcsNodeManage[m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sPhysicalNode - 700].eNodeStatus = NODE_FAILED;

            //                    break;

            //                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGOUT:
            //                    //m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].eLoginStatus status unchanged;
            //                    sprintf(m_szTrace, "VCS LOGOUT FAILED (Node %d, Exercise %d) \n", m_pVcsExManage[sAMSExerciseNo].Roles[nRoleIdx].sPhysicalNode, sAMSExerciseNo);
            //                    Console.WriteLine("");
            //                    break;
            //            }
            //            break;


            //            //return (m_tVcsIfManage.nTransmittedVICExerciseNo * nQueueRemovalEnable);
            //            return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //            break;

            //		/************ Error Report *************/

            //		case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ERROR_REPORT:

            //            SendStatusRequest();

            //            pDataSegment[sDataSize - 1] = '\0'; //remove CR, LF
            //            if (strlen(pDataSegment) > 111)
            //                pDataSegment[111] = '\0';

            //            strcpy(szErrorString, "VCS information: ");
            //            String.Concat(szErrorString, pDataSegment);

            //            SendLoggerMessage(T_VCS_ERR, m_tVcsIfManage.nTransmittedVICExerciseNo, szErrorString);
            //            break;	

            //		case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_INVALID_MSG:

            //            sprintf(szTemp1, "VCS reported invalid message (ID %d)", *pusStatus);
            //            SendLoggerMessage(T_VCS_ERR, m_tVcsIfManage.nTransmittedVICExerciseNo, szTemp1);
            //            break;

            //		/************ Trainee Monitoring *************/

            //		case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_MONITOR_START:

            //            pMonPDU = new msgVCSMonitor_t;

            //            pMonPDU->nSupervisorPhysicalNode = (int)pusStatus++;
            //            pMonPDU->nSupervisorPhysicalNode -= (pMonPDU->nSupervisorPhysicalNode >= 700) ? 700 : 0;

            //            pMonPDU->nTraineePhysicalNode = (int)pusStatus;
            //            pMonPDU->nTraineePhysicalNode -= (pMonPDU->nTraineePhysicalNode >= 700) ? 700 : 0;

            //            /* Find active exercise */
            //            for (n = 0; n < m_sTotalExerciseCount; n++)
            //            {
            //                bFoundTrainNode = bFoundInstrNode = false;

            //                if (m_pVcsExManage[n].eED_State != ED_UNDEFINED)
            //                {
            //                    for (i = 0; i < m_pVcsExManage[n].sRoleCount; i++)
            //                    {
            //                        if (((m_pVcsExManage[n].Roles[i].sPhysicalNode - 700) == (short)pMonPDU->nSupervisorPhysicalNode) &&
            //                                (CSystemComponent::SPARK_INSTRUCTOR == m_pVcsExManage[n].Roles[i].sRoleType))
            //                            bFoundInstrNode = true;


            //                        if (((m_pVcsExManage[n].Roles[i].sPhysicalNode - 700) == (short)pMonPDU->nTraineePhysicalNode) &&
            //                                (CSystemComponent::SPARK_TRAINEE_OPERATOR == m_pVcsExManage[n].Roles[i].sRoleType))
            //                            bFoundTrainNode = true;

            //                        if (bFoundTrainNode && bFoundInstrNode)
            //                            break;
            //                    }
            //                }

            //                if (bFoundTrainNode && bFoundInstrNode)
            //                    break;
            //            }

            //            /* Send PDU (if valid request) and reply to VCS */
            //            if (bFoundTrainNode && bFoundInstrNode)
            //            {
            //                pMonPDU->lMessageID = eMsg_VCS_MONITOR;
            //                pMonPDU->bMonitorState = true;
            //                pMonPDU->nTraineeExerciseID = n;

            //                offset = pMonPDU->nSupervisorPhysicalNode - m_sTotalNumTrainees - 1;

            //                if (m_pVcsExManage[n].bMonitorActiveSupervisor[offset])
            //                    SendLoggerMessage(T_AMS_IFM, -2, "Invalid monitor start message received");

            //                m_pVcsExManage[n].bMonitorActiveSupervisor[offset] = true;
            //                m_pVcsExManage[n].sMonitorActiveTrainee[offset] = pMonPDU->nTraineePhysicalNode;

            //                strcpy(pMonPDU->szTraineePlatform, m_pVcsExManage[n].Roles[i].szPlatform);

            //                /* Send Message to Sys Admin */
            //#if _VIC_CMND_STATUS_TRACE
            //					sprintf(m_szTrace, "VCS Monitor Enabled - Instructor %d, Trainee: %d, \n", pMonPDU->nSupervisorPhysicalNode, pMonPDU->nTraineePhysicalNode);
            //					Console.WriteLine("");
            //#endif

            //                m_pSEI->SendMessage(eMsg_VCS_MONITOR, pMonPDU->nTraineeExerciseID, SEI_BROADCAST, false, sizeof(eMsg_VCS_MONITOR), pMonPDU);
            //                SendVCSStatusReply(A_MONITOR_START, VCS_SUCCESS);

            //            }
            //            else
            //            {
            //                SendVCSStatusReply(A_MONITOR_START, VCS_INV_POS);
            //                SendLoggerMessage(T_AMS_IFM, -2, "Invalid monitor start message received (invalid position)");
            //            }


            //            //free (pMonPDU); crashes?
            //            return -2;

            //            break;

            //		case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_MONITOR_END:

            //            pMonPDU = new msgVCSMonitor_t;

            //            pMonPDU->nSupervisorPhysicalNode = (int)*pusStatus;
            //            pMonPDU->nSupervisorPhysicalNode -= (pMonPDU->nSupervisorPhysicalNode >= 700) ? 700 : 0;

            //            offset = pMonPDU->nSupervisorPhysicalNode - m_sTotalNumTrainees - 1;

            //            /* Find active exercise */
            //            for (n = 0; n < m_sTotalExerciseCount; n++)
            //            {
            //                if ((m_pVcsExManage[n].eED_State != ED_UNDEFINED) &&
            //                        (m_pVcsExManage[n].bMonitorActiveSupervisor[offset]))
            //                {
            //                    pMonPDU->lMessageID = eMsg_VCS_MONITOR;
            //                    pMonPDU->nTraineePhysicalNode = (int)m_pVcsExManage[n].sMonitorActiveTrainee[offset];
            //                    pMonPDU->bMonitorState = false;
            //                    pMonPDU->nTraineeExerciseID = n;
            //                    strcpy(pMonPDU->szTraineePlatform, "");

            //                    /* Send Message to Sys Admin */
            //#if _VIC_CMND_STATUS_TRACE
            //						sprintf(m_szTrace, "VCS Monitor Disabled - Instructor %d, Trainee: %d, \n", pMonPDU->nSupervisorPhysicalNode, pMonPDU->nTraineePhysicalNode);
            //						Console.WriteLine("");
            //#endif

            //                    m_pVcsExManage[n].bMonitorActiveSupervisor[offset] = false;
            //                    m_pVcsExManage[n].sMonitorActiveTrainee[offset] = 0;

            //                    m_pSEI->SendMessage(eMsg_VCS_MONITOR, pMonPDU->nTraineeExerciseID, SEI_BROADCAST, false, sizeof(eMsg_VCS_MONITOR), pMonPDU);
            //                    SendVCSStatusReply(A_MONITOR_END, VCS_SUCCESS);
            //                    break;
            //                }
            //            }
            //            /* If no reply sent then send error message */
            //            if (n == m_sTotalExerciseCount)
            //            {
            //                SendVCSStatusReply((Int16)UNET_Classes.Enums.SIM_Message_IDs.A_MONITOR_END, VCS_INV_POS);
            //                SendLoggerMessage(T_AMS_IFM, -2, "VCS monitor end - failed");
            //            }

            //            //free (pMonPDU); 
            //            return -2;

            //            break;	

            //		/************ IMPOSITION *************/

            //			case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_IMPOSE_START:
            //			case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_IMPOSE_END:

            //            usVcsExerciseNo = pusStatus++; //Supervisor physical position
            //            usStatus = *pusStatus;

            //            switch (usStatus)
            //            {
            //                case VCS_SUCCESS:

            //                    if (rxdMessageId == A_IMPOSE_START)
            //                        Console.WriteLine("VCS IMPOSITION ON\n");
            //                    else
            //                        Console.WriteLine("VCS IMPOSITION OFF\n");
            //                    break;

            //                default:    //A_IMPOSE_START	-		FAILURE, INV_SVR_POS
            //                            //A_IMPOSE_END		-		FAILURE, INV_SVR_POS, INV_POS

            //                    SendLoggerMessage(T_AMS_IFM, m_tVcsIfManage.nTransmittedVICExerciseNo, GetVCSError(rxdMessageId, usStatus, eERROR));
            //                    break;
            //            }
            //            //return (m_tVcsIfManage.nTransmittedVICExerciseNo * nQueueRemovalEnable);
            //            return m_tVcsIfManage.nTransmittedVICExerciseNo;

            //            break;


            //            ///************ Seek *************/

            //            //case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_SEEK:
            //            //          usVcsExerciseNo = *pusStatus++;
            //            //          usStatus = *pusStatus++;

            //            //          switch (usStatus)
            //            //          {

            //            //              case VCS_SUCCESS:
            //            //                  //default:				//TEMP FIX UNTIL NEW R/R Code Recieved

            //            //                  /* The seek reply can be unsolicited as a result of a ready message being received 
            //            //                  from Record/Replay PC (time adjustment)  Determine if solicited message and trace timestamps */

            //            //                  if ((m_tVcsIfManage.nTransmittedVICExerciseNo == -2) ||
            //            //                          (m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].pMsgQueue == NULL) ||
            //            //                          (rxdMessageId != m_pVcsExManage[m_tVcsIfManage.nTransmittedVICExerciseNo].pMsgQueue->sMsgExpectedReply))
            //            //                  {
            //            //                      sprintf(m_szVCSError, "VCS Seeked [Unsolicited] Exercise Time:");
            //            //                      n = -2;
            //            //                  }
            //            //                  else
            //            //                  {
            //            //                      UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FROZEN);
            //            //                      sprintf(m_szVCSError, "VCS Seeked Exercise Time: ");
            //            //                      n = m_tVcsIfManage.nTransmittedVICExerciseNo;
            //            //                  }

            //            //                  /* Extract scenario and exercise time	*/
            //            //                  memcpy((void)szTemp1, (void)pusStatus, 12);
            //            //                  szTemp1[12] = '\0';

            //            //                  String.Concat(m_szVCSError, szTemp1);
            //            //                  String.Concat(m_szVCSError, ",  Scenario Time: ");

            //            //                  pusStatus += 6;

            //            //                  memcpy((void)szTemp1, (void)pusStatus, 12);
            //            //                  szTemp1[12] = '\n';
            //            //                  szTemp1[13] = '\0';

            //            //                  /* Debug info */
            //            //                  String.Concat(m_szVCSError, szTemp1);
            //            //                  Console.WriteLine(m_szVCSError);


            //            //                  //UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FROZEN);

            //            //                  return n;

            //            //                  break;

            //            //              default:    //A_EC_SEEK		-		FAILURE, INV_EX_NUM, INV_EX_TIME, INV_SC_TIME, INV_STATE

            //            //                  UpdateExerciseStatus(m_tVcsIfManage.nTransmittedVICExerciseNo, CScenarioComponent::FAULT, GetVCSError(rxdMessageId, usStatus, eERROR));
            //            //                  return m_tVcsIfManage.nTransmittedVICExerciseNo;
            //            //                  break;
            //            //          }
            //            //          break;

            //            //          /************ Unexpected messages *************/
            //            //          default:
            //            ///*
            //            //	Unused replies:-	A_GET_EX_BY_ID:		
            //            //										A_GET_EX_BY_NAME:
            //            //										A_EC_EX_STATE:
            //            //*/
            //            //	SendLoggerMessage(T_AMS_IFM, -2, "Unknown reply from VCS");
            //            //          break;

            //        }
            /* Attempt to send another queued message here*/
            //  SendNextQueuedMessage();

            return -2;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : SendVCSStatusReply
        // Description: Sends an appropriate response message back to the VCS
        ////////////////////////////////////////////////////////////////////////////////
        public void SendVCSStatusReply(ushort MessageID, ushort MessageStatus)
        {
            //QueuedMsg_t newMsg;
            //ushort psData;

            //if (WaitForSingleObject(m_pVcsExManage[0].hQueueMutex, 2000) == WAIT_TIMEOUT)
            //{
            //    SendLoggerMessage(T_AMS_IFM, -2, "Unable to send status reply");
            //    return;
            //}

            //if (!(newMsg = AddMessageToQueue(0, F_SENDSTAT)))
            //{
            //    SendLoggerMessage(T_AMS_IFM, -2, "Unable to send status reply");
            //    return;
            //}

            //newMsg->MsgHeader.sID = MessageID;
            //newMsg->MsgHeader.sReserved = 0;

            //newMsg->sMsgExpectedReply = A_NONE;

            //psData = (ushort)&newMsg->MsgData;

            //psData = MessageStatus;

            //newMsg->MsgHeader.usLength = 2;
            //newMsg->nMsgSize = newMsg->MsgHeader.usLength + sizeof(vcsHeader_t);
            //m_pVcsExManage[0].nMsgCnt++;

            ///* Signal message ready to send */
            //m_pVcsExManage[0].eMsgStatus = MESSAGE_READY;

            //ReleaseMutex(m_pVcsExManage[0].hQueueMutex);

            ///* Will only send this message here if not awaiting reply to another message */
            ///* Otherwise message will be sent once awaited reply received */
            //if (m_tVcsIfManage.eVCS_LinkState == READY_CONNECT)
            //    SendMsgToVCS(0, m_pVcsExManage[0].pMsgQueue, eTHREAD_IFM);

            return;
        }


        ////////////////////////////////////////////////////////////////////////////////
        // Function   : SendWOLMessage
        // Description: Sends a Magic Packet to Wake-On-LAN a PC in standby state
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5940037
        public void SendWOLMessage(char szMAC_ADDRESS)
        {
            //   char WOL_ADDRESS[] = "255.255.255.255";
            //   ushort WOL_PORT_NUMBER = 1026;

            //   char cbuf[102];
            //   int nBytesSent;

            //   int sockOption = 1;

            //   /* Create a connectionless socket for WOL command */
            //   clientSocket = socket(AF_INET, SOCK_DGRAM, 0);      //UDP

            //   if (setsockopt(clientSocket, SOL_SOCKET, SO_BROADCAST, (const char FAR *) &sockOption, sizeof(sockOption)))
            //{
            //       TRACE("VCS Failed to set broadcast socket\n");
            //       return;
            //   }

            //   serverAddr.sin_family = AF_INET;
            //   serverAddr.sin_addr.s_addr = inet_addr(WOL_ADDRESS);
            //   serverAddr.sin_port = htons(WOL_PORT_NUMBER);


            //   if (CreateWOLMessage(cbuf, szMAC_ADDRESS))
            //       TRACE("VCS Failed To create mac address\n");
            //   else
            //   {
            //       nBytesSent = sendto(clientSocket, (const char*) &cbuf[0], 102, 0, (struct sockaddr*)&serverAddr, sizeof(serverAddr)) ;

            //	if (nBytesSent != 102)

            //           TRACE("VCS Failed To send data\n");
            //}

            //   /* Close socket */
            //   closesocket(clientSocket);

        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : ConvertMacAddress
        // Description: Converts a NIC MAC Address from string to integer format
        ////////////////////////////////////////////////////////////////////////////////

        //##ModelId=4119F5940073
        public bool CreateWOLMessage(char cbuff, char szMAC)
        {
            //int i, j;
            // char macAddress[6];

            ///* Convert MAC string to integer array*/
            //for (i = 0; i < 6; i++)
            //{
            //    for (j = 0; j < 3; j++)
            //    {
            //        if (j == 0)
            //        {
            //            if ('0' <= *szMAC && *szMAC <= '9')
            //                macAddress[i] = (*szMAC - '0') << 4;
            //            else if ('a' <= *szMAC && *szMAC <= 'f')
            //                macAddress[i] = (*szMAC - 'a' + 10) << 4;
            //            else if ('A' <= *szMAC && *szMAC <= 'F')
            //                macAddress[i] = (*szMAC - 'A' + 10) << 4;
            //            else
            //                return true;
            //        }
            //        else if (j == 1)
            //        {
            //            if ('0' <= *szMAC && *szMAC <= '9')
            //                macAddress[i] += *szMAC - '0';
            //            else if ('a' <= *szMAC && *szMAC <= 'f')
            //                macAddress[i] += *szMAC - 'a' + 10;
            //            else if ('A' <= *szMAC && *szMAC <= 'F')
            //                macAddress[i] += *szMAC - 'A' + 10;
            //            else
            //                return true;
            //        }
            //        else if ((j == 2) && (i != 5) && *szMAC != ':')
            //            return true;

            //        szMAC++;
            //    }
            //}

            ///* WOL synchronisation stream */
            //for (i = 0; i < 6; i++)
            //    *cbuff++ = (char)0xff;

            ///* 16 duplications of MAC address */
            //for (i = 0; i < 16; i++)
            //{
            //    for (j = 0; j < 6; j++)
            //        *cbuff++ = (char)macAddress[j];
            //}

            return false;
        }

        ////////////////////////////////////////////////////////////////////////////////
        // Function   : GetVCSError
        // Description: Primary function is to update the m_szVCSError attribute with a 
        //							deciphered error message using supplied message and error codes
        //							(ErrorType = eERROR). Can also be used to create debug msg tx/rx strings
        //							(ErrorType = eTX and eRX) for switches _VCS_INTERCHANGE_TRACE and
        //							_VCS_SYNC_STATUS_TRACE or convert a message code to an equivalent
        //							string (ErrorType = eNAME)
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F59303CF
        //todo: de string functieresult, was een char
        public string GetVCSError(short Reply, short ErrorCode, UNET_Classes.Enums.eErrorType ErrorType)
        {
            string szLocal = string.Empty;//[40] = "";
            string m_szVCSError = string.Empty;
            switch (ErrorType)
            {
                case UNET_Classes.Enums.eErrorType.eERROR:
                    String.Concat(m_szVCSError, "VCS reported error: ");
                    break;

                case UNET_Classes.Enums.eErrorType.eTX:
                    if (ErrorCode < 0)
                    {
                        string.Format(m_szVCSError, "VCS TX\t\t\t\t\t[");
                    }
                    else
                    {
                        string.Format(m_szVCSError, "VCS TX\t  [Exercise ");
                        string.Concat(m_szVCSError, ErrorCode, CommsControl.Pad_2);
                        String.Concat(m_szVCSError, "] [");
                    }
                 //todo   AppendCurrentTimeStr(m_szVCSError);
                    String.Concat(m_szVCSError, "] - ");

                    //if (I_CONNECTIVITY == Reply)
                    //	Sleep(100); //temporary

                    break;

                case UNET_Classes.Enums.eErrorType.eRX:
                    string.Format(m_szVCSError, "VCS RX\t\t\t\t\t[");
                 //todo   AppendCurrentTimeStr(m_szVCSError);
                    String.Concat(m_szVCSError, "] - ");
                    break;

                case UNET_Classes.Enums.eErrorType.eNAME:
                    string.Format(m_szVCSError, "");
                    break;
            }

            switch (Reply)
            {
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_LOAD:
                    string.Concat(m_szVCSError, "I_EC_LOAD");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_LOAD:
                    String.Concat(m_szVCSError, "A_EC_LOAD");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_START:
                    String.Concat(m_szVCSError, "I_EC_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_START:
                    String.Concat(m_szVCSError, "A_EC_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_STOP:
                    String.Concat(m_szVCSError, "I_EC_STOP");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_STOP:
                    String.Concat(m_szVCSError, "A_EC_STOP");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_PAUSE:
                    String.Concat(m_szVCSError, "I_EC_PAUSE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PAUSE:
                    String.Concat(m_szVCSError, "A_EC_PAUSE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_PLAYBACK:
                    String.Concat(m_szVCSError, "I_EC_PLAYBACK");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_PLAYBACK:
                    String.Concat(m_szVCSError, "A_EC_PLAYBACK");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_SEEK:
                    String.Concat(m_szVCSError, "I_EC_SEEK");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_SEEK:
                    String.Concat(m_szVCSError, "A_EC_SEEK");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ERROR_REPORT:
                    String.Concat(m_szVCSError, "I_ERROR_REPORT");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ES_CONFIG:
                    String.Concat(m_szVCSError, "I_ES_CONFIG");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_CONFIG:
                    String.Concat(m_szVCSError, "A_ES_CONFIG");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ES_DEL:
                    String.Concat(m_szVCSError, "I_ES_DEL");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_DEL:
                    String.Concat(m_szVCSError, "A_ES_DEL");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ES_END:
                    String.Concat(m_szVCSError, "I_ES_END");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_END:
                    String.Concat(m_szVCSError, "A_ES_END");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_RP_CONFIG:
                    String.Concat(m_szVCSError, "I_EC_RP_CONFIG");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RP_CONFIG:
                    String.Concat(m_szVCSError, "A_EC_RP_CONFIG");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ES_START:
                    String.Concat(m_szVCSError, "I_ES_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_START:
                    String.Concat(m_szVCSError, "A_ES_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_SC_RU_ALIVE:
                    String.Concat(m_szVCSError, "I_SC_RU_ALIVE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RU_ALIVE:
                    String.Concat(m_szVCSError, "A_SC_RU_ALIVE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_TIME_DATE:
                    String.Concat(m_szVCSError, "I_TIME_DATE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_TIME_DATE:
                    String.Concat(m_szVCSError, "A_TIME_DATE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_INVALID_MSG:
                    String.Concat(m_szVCSError, "A_INVALID_MSG");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_LOGIN:
                    String.Concat(m_szVCSError, "I_LOGIN");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGIN:
                    String.Concat(m_szVCSError, "A_LOGIN");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_LOGOUT:
                    String.Concat(m_szVCSError, "I_LOGOUT");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_LOGOUT:
                    String.Concat(m_szVCSError, "A_LOGOUT");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_ES_ROLE_DEF:
                    String.Concat(m_szVCSError, "I_ES_ROLE_DEF");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_ES_ROLE_DEF:
                    String.Concat(m_szVCSError, "A_ES_ROLE_DEF");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_MONITOR_START:
                    String.Concat(m_szVCSError, "I_MONITOR_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_MONITOR_START:
                    String.Concat(m_szVCSError, "A_MONITOR_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_MONITOR_END:
                    String.Concat(m_szVCSError, "I_MONITOR_END");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_MONITOR_END:
                    String.Concat(m_szVCSError, "A_MONITOR_END");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_IMPOSE_START:
                    String.Concat(m_szVCSError, "I_IMPOSE_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_IMPOSE_START:
                    String.Concat(m_szVCSError, "A_IMPOSE_START");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_IMPOSE_END:
                    String.Concat(m_szVCSError, "I_IMPOSE_END");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_IMPOSE_END:
                    String.Concat(m_szVCSError, "A_IMPOSE_END");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_REPLAY:
                    String.Concat(m_szVCSError, "I_EC_REPLAY");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_REPLAY:
                    String.Concat(m_szVCSError, "A_EC_REPLAY");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_RESUME:
                    String.Concat(m_szVCSError, "I_EC_RESUME");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_RESUME:
                    String.Concat(m_szVCSError, "A_EC_RESUME");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_ALLOC:
                    String.Concat(m_szVCSError, "I_EC_ALLOC");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_ALLOC:
                    String.Concat(m_szVCSError, "A_EC_ALLOC");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_SC_RESET:
                    String.Concat(m_szVCSError, "I_SC_RESET");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_RESET:
                    String.Concat(m_szVCSError, "A_SC_RESET");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_GET_EX_BY_ID:
                    String.Concat(m_szVCSError, "I_GET_EX_BY_ID");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_GET_EX_BY_ID:
                    String.Concat(m_szVCSError, "A_GET_EX_BY_ID");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_GET_EX_BY_NAME:
                    String.Concat(m_szVCSError, "I_GET_EX_BY_NAME");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_GET_EX_BY_NAME:
                    String.Concat(m_szVCSError, "A_GET_EX_BY_NAME");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_EC_EX_STATE:
                    String.Concat(m_szVCSError, "I_EC_EX_STATE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_EC_EX_STATE:
                    String.Concat(m_szVCSError, "A_EC_EX_STATE");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_CONNECTIVITY:
                    String.Concat(m_szVCSError, "I_CONNECTIVITY");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_SC_SHUTDOWN:
                    String.Concat(m_szVCSError, "I_SC_SHUTDOWN");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.A_SC_SHUTDOWN:
                    String.Concat(m_szVCSError, "A_SC_SHUTDOWN");
                    break;
                case (Int16)UNET_Classes.Enums.SIM_Message_IDs.I_POWER_OFF:
                    String.Concat(m_szVCSError, "I_POWER_OFF");
                    break;
                default:
                    string.Format(szLocal, "UNKNOWN MESSAGE (%d)", Reply);
                    String.Concat(m_szVCSError, szLocal);
                    break;
            }

            if (ErrorType != UNET_Classes.Enums.eErrorType.eERROR)
            {
                if (ErrorType != UNET_Classes.Enums.eErrorType.eNAME)
                    String.Concat(m_szVCSError, "\n");

                return m_szVCSError;
            }

            switch (ErrorCode)
            {
                case CommsControl.AMS_NO_ERROR:
                    break;
                case CommsControl.VCS_ES_BUSY:
                    String.Concat(m_szVCSError, " - VCS_ES_BUSY");
                    break;
                case CommsControl.VCS_EX_LOCAL:
                    String.Concat(m_szVCSError, " - VCS_EX_LOCAL");
                    break;
                case CommsControl.VCS_EX_NOT_LOADED:
                    String.Concat(m_szVCSError, " - VCS_EX_NOT_LOADED");
                    break;
                case CommsControl.VCS_FAILURE:
                    String.Concat(m_szVCSError, " - VCS_FAILURE");
                    break;
                case CommsControl.VCS_INV_DATE:
                    String.Concat(m_szVCSError, " - VCS_INV_DATE");
                    break;
                case CommsControl.VCS_INV_ES_DATA:
                    String.Concat(m_szVCSError, " - VCS_INV_ES_DATA");
                    break;
                case CommsControl.VCS_INV_EX_NAME:
                    String.Concat(m_szVCSError, " - VCS_INV_EX_NAME");
                    break;
                case CommsControl.VCS_INV_EX_NUM:
                    String.Concat(m_szVCSError, " - VCS_INV_EX_NUM");
                    break;
                case CommsControl.VCS_INV_EX_TIME:
                    String.Concat(m_szVCSError, " - VCS_INV_TIME");
                    break;
                case CommsControl.VCS_INV_EQUIP:
                    String.Concat(m_szVCSError, " - VCS_INV_EQUIP");
                    break;
                case CommsControl.VCS_INV_POS:
                    String.Concat(m_szVCSError, " - VCS_INV_POS");
                    break;
                case CommsControl.VCS_INV_ROLE_ID:
                    String.Concat(m_szVCSError, " - VCS_INV_ROLE_ID");
                    break;
                case CommsControl.VCS_INV_STATE:
                    String.Concat(m_szVCSError, " - VCS_INV_STATE");
                    break;
                case CommsControl.VCS_INV_TIME:
                    String.Concat(m_szVCSError, " - VCS_INV_TIME");
                    break;
                case CommsControl.VCS_INV_EX_TYPE:
                    String.Concat(m_szVCSError, " - VCS_INV_EX_TYPE");
                    break;
                case CommsControl.VCS_INV_FILE:
                    String.Concat(m_szVCSError, " - VCS_INV_FILE");
                    break;
                case CommsControl.VCS_INV_SVR_POS:
                    String.Concat(m_szVCSError, " - VCS_INV_SVR_POS");
                    break;
                case CommsControl.VCS_INV_SC_TIME:
                    String.Concat(m_szVCSError, " - VCS_INV_SC_TIME");
                    break;
                case CommsControl.AMS_WRONG_EXERCISE:
                    String.Concat(m_szVCSError, " - AMS_WRONG_EXERCISE");
                    break;
                case CommsControl.AMS_UNKNOWN_ROLE:
                    String.Concat(m_szVCSError, " - AMS_UNKNOWN_ROLE");
                    break;
                default:
                    string.Concat(szLocal, " - UNKNOWN ERROR (%d)", ErrorCode);
                    String.Concat(m_szVCSError, szLocal);
                    break;
            }

            return m_szVCSError;
        }
        ////////////////////////////////////////////////////////////////////////////////
        // Function   : GetWSAError
        // Description: Returns WSA Error code minus base offset
        ////////////////////////////////////////////////////////////////////////////////
        //##ModelId=4119F59400CE
        public int GetWSAError(int LastError)
        {

            // LastError -= WSABASEERR;

            return LastError;

            /* Retained for debug info
            #define WSAEINTR:               (WSABASEERR+4) 
            #define WSAEBADF:               (WSABASEERR+9)	
            #define WSAEACCES:              (WSABASEERR+13)
            #define WSAEFAULT:              (WSABASEERR+14)
            #define WSAEINVAL:              (WSABASEERR+22)
            #define	WSAEMFILE								(WSABASEERR+24):			
            #define WSAEWOULDBLOCK          (WSABASEERR+35)
            #define WSAEINPROGRESS          (WSABASEERR+36)
            #define WSAEALREADY             (WSABASEERR+37)
            #define WSAENOTSOCK             (WSABASEERR+38)
            #define WSAEDESTADDRREQ         (WSABASEERR+39)
            #define WSAEMSGSIZE             (WSABASEERR+40)
            #define WSAEPROTOTYPE           (WSABASEERR+41)
            #define WSAENOPROTOOPT          (WSABASEERR+42)
            #define WSAEPROTONOSUPPORT      (WSABASEERR+43)
            #define WSAESOCKTNOSUPPORT      (WSABASEERR+44)
            #define WSAEOPNOTSUPP           (WSABASEERR+45)
            #define WSAEPFNOSUPPORT         (WSABASEERR+46)
            #define WSAEAFNOSUPPORT         (WSABASEERR+47)
            #define WSAEADDRINUSE           (WSABASEERR+48)
            #define WSAEADDRNOTAVAIL        (WSABASEERR+49)
            #define WSAENETDOWN             (WSABASEERR+50)
            #define WSAENETUNREACH          (WSABASEERR+51)
            #define WSAENETRESET            (WSABASEERR+52)
            #define WSAECONNABORTED         (WSABASEERR+53)
            #define WSAECONNRESET           (WSABASEERR+54)
            #define WSAENOBUFS              (WSABASEERR+55)
            #define WSAEISCONN              (WSABASEERR+56)
            #define WSAENOTCONN             (WSABASEERR+57)
            #define WSAESHUTDOWN            (WSABASEERR+58)
            #define WSAETOOMANYREFS         (WSABASEERR+59)
            #define WSAETIMEDOUT            (WSABASEERR+60)
            #define WSAECONNREFUSED         (WSABASEERR+61)
            #define WSAELOOP                (WSABASEERR+62)
            #define WSAENAMETOOLONG         (WSABASEERR+63)
            #define WSAEHOSTDOWN            (WSABASEERR+64)
            #define WSAEHOSTUNREACH         (WSABASEERR+65)
            #define WSAENOTEMPTY            (WSABASEERR+66)
            #define WSAEPROCLIM             (WSABASEERR+67)
            #define WSAEUSERS               (WSABASEERR+68)
            #define WSAEDQUOT               (WSABASEERR+69)
            #define WSAESTALE               (WSABASEERR+70)
            #define WSAEREMOTE              (WSABASEERR+71)	
            */
        }
    }
}		
