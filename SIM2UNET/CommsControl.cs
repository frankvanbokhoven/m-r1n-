﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIM2VOIP
{
    public static class CommsControl
    {
        ///zoveel mogelijk overgenomen van CommsControl.h

        ////////////////////////////////////////////////////////////////////////////////
        // File: CommsControl.h
        //
        // Generated by : thomas askew
        // Generation date : 10:00:00 23/12/2002
        //
        // Copyright Alenia Marconi Systems Ltd 2002

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

        Module Title        : CommsComponent.h

        Classification      : Unclassified

        Language            : C++

        Operating System    : Microsoft Windows 2000

        Design Document Ref : See Module Specification below

        SDA                 : Derek Walker
        ------------------------------------------------------------------------------
        Version Control(VSS)
        ---------------------
        $Revision: 5 $
        $Date: 7/01/05 15:24 $
        $Author: Gerard.magee $
        $Workfile: CommsControl.h $
        ------------------------------------------------------------------------------
        Module Specification : 

        UNET-MS-090 EW Trainer To TroyCommunications System Interface Specification
        UNET-MS-104 Voice Communication System Communications Interface Panel MMI
        UNET-MS-134 VCS Interface PDUs

        ------------------------------------------------------------------------------
        Module Description   : 

        Header file

        ------------------------------------------------------------------------------*/


        public static int TOTAL_NUM_CONTROL_PC = 4;                                 //System PC, Download PC, Record/Replay PC, Archive PC
        public static int IL_IDX = 0;
        public static int TEST_IDX = 1;
        public static int GEN_IDX = -1;
        public static int NO_EXERCISE = -2;
        public static int DESK_OFFSET = 700;
        public static int MAGIC_PACKET_SIZE = 102;
        public static int MAX_NUM_NODES = 21;
        public static int MAX_NUM_RADIOS = 20;
        public static int MAX_NUM_INSTRUCTORS = 4;
        public static int MAX_NUM_ACTORS = 20;
        public static int MAX_NUM_DESKS = 8;                               //Trainee desks
        public static int MAX_NUM_EXERCISES = 10;      //For UNET = 8 x Multiple, 1 x IL and 1 x General
                                                //For SEWT = 8 x Radar/EW Only and 1 x General

        public static int REPLAY_IDX = 8 + 1;// MAX_NUM_DESKS + 1;  //Exercise 9 reserved for replay
        public static int MAX_EXERCISE_COUNT = 8 + 2;// MAX_NUM_DESKS + 2;   //Exercise 0 reserved for IL and Non-exercise messages

        /* Definitions - Watchdog */
        public static int WD_RUALIVE_NOREPLY = 0;
        public static int WD_RECONNECT = 1;
        public static int WD_EXERCISE_NOREPLY = 5;

        public static int NOTIFY_ON_CONNECT_RETRY_COUNT = 8;

        /* Definitions - Record/Replay Enable */
        public static bool RR_ACTIVATED = true;
        public static bool RR_DEACTIVATED = false;

        public static bool WITH_STOP = true;
        public static bool WITHOUT_STOP = false;

        public static bool M_ERROR = true;
        public static bool M_SUCCESS = false;


        public static bool VCS_CONTROL_FAULT = true;
        public static bool VCS_CONTROL_NO_FAULT = false;

        public static int VCS_INSTRUCTOR_FAULT = 1;
        public static int VCS_INSTRUCTOR_NO_FAULT = 0;

        /* AppendIntStr Padding */
        public static int Pad_0 = 0;
        public static int Pad_1 = 1;
        public static int Pad_2 = 2;
        public static int Pad_3 = 3;
        public static int Pad_4 = 4;

        /* Definitions - Interface Control */
        public static int RX_DATA_SEGMENT_SIZE = 200;
        public static int TX_DATA_SEGMENT_SIZE = 10000;

        public static int I_EC_LOAD = unchecked(0x0A00);
        public static int A_EC_LOAD = unchecked(0x0A01);
        public static int I_EC_START = unchecked(0x0A02);
        public static int A_EC_START = unchecked(0x0A03);
        public static int I_EC_STOP = unchecked(0x0A04);
        public static int A_EC_STOP = unchecked(0x0A05);
        public static int I_EC_PAUSE = unchecked(0x0A06);
        public static int A_EC_PAUSE = unchecked(0x0A07);
        public static int I_EC_PLAYBACK = unchecked(0x0A08);
        public static int A_EC_PLAYBACK = unchecked(0x0A09);
        public static int I_EC_SEEK = unchecked(0x0A0A);
        public static int A_EC_SEEK = unchecked(0x0A0B);
        public static int I_ERROR_REPORT = unchecked(0x0D0C);  //Handle VCS Coding Error SPC 4.108i, DL 2.36n
        public static int I_ES_CONFIG = unchecked(0x0A0D);
        public static int A_ES_CONFIG = unchecked(0x0A0E);
        public static int I_ES_DEL = unchecked(0x0A10);
        public static int A_ES_DEL = unchecked(0x0A11);
        public static int I_ES_END = unchecked(0x0A12);
        public static int A_ES_END = unchecked(0x0A13);
        public static int I_EC_RP_CONFIG = unchecked(0x0A14);
        public static int A_EC_RP_CONFIG = unchecked(0x0A15);
        public static int I_ES_START = unchecked(0x0A16);
        public static int A_ES_START = unchecked(0x0A17);
        public static int I_GET_EX_BY_ID = unchecked(0x0A18);
        public static int A_GET_EX_BY_ID = unchecked(0x0A19);
        public static int I_GET_EX_BY_NAME = unchecked(0x0A1A);
        public static int A_GET_EX_BY_NAME = unchecked(0x0A1B);
        public static int I_SC_RU_ALIVE = unchecked(0x0A1C);
        public static int A_SC_RU_ALIVE = unchecked(0x0A1D);
        public static int I_TIME_DATE = unchecked(0x0A20);
        public static int A_TIME_DATE = unchecked(0x0A21);
        public static int A_INVALID_MSG = unchecked(0x0A22);
        public static int I_SC_RESET = unchecked(0x0A23);
        public static int A_SC_RESET = unchecked(0x0A24);
        public static int I_EC_EX_STATE = unchecked(0x0A25);
        public static int A_EC_EX_STATE = unchecked(0x0A26);
        public static int I_LOGIN = unchecked(0x0A27);
        public static int A_LOGIN = unchecked(0x0A28);
        public static int I_LOGOUT = unchecked(0x0A29);
        public static int A_LOGOUT = unchecked(0x0A2A);
        public static int I_ES_ROLE_DEF = unchecked(0x0A2B);
        public static int A_ES_ROLE_DEF = unchecked(0x0A2C);
        public static int I_MONITOR_START = unchecked(0x0A2D);
        public static int A_MONITOR_START = unchecked(0x0A2E);
        public static int I_MONITOR_END = unchecked(0x0A30);
        public static int A_MONITOR_END = unchecked(0x0A31);
        public static int I_IMPOSE_START = unchecked(0x0A32);
        public static int A_IMPOSE_START = unchecked(0x0A33);
        public static int I_IMPOSE_END = unchecked(0x0A34);
        public static int A_IMPOSE_END = unchecked(0x0A35);
        public static int I_EC_REPLAY = unchecked(0x0A36);
        public static int A_EC_REPLAY = unchecked(0x0A37);
        public static int I_EC_RESUME = unchecked(0x0A38);
        public static int A_EC_RESUME = unchecked(0x0A39);
        public static int I_EC_ALLOC = unchecked(0x0A3A);
        public static int A_EC_ALLOC = unchecked(0x0A3B);
        public static int I_CONNECTIVITY = unchecked(0x0A41);
        public static int I_SC_SHUTDOWN = unchecked(0x0A42);
        public static int A_SC_SHUTDOWN = unchecked(0x0A43);
        public static int I_POWER_OFF = unchecked(0x0A44);
        public static int A_NONE = unchecked(0x0AAA);

        public static int F_RESET = unchecked(0x0B00);
        public static int F_LOAD = unchecked(0x0B01);
        public static int F_START = unchecked(0x0B02);
        public static int F_PAUSE = unchecked(0x0B03);
        public static int F_STOP = unchecked(0x0B04);
        public static int F_LOGIN = unchecked(0x0B05);
        public static int F_ILOGIN = unchecked(0x0B06);
        public static int F_LOGOUT = unchecked(0x0B07);
        public static int F_COMMSCOMF = unchecked(0x0B08);
        public static int F_RUNCONTROL = unchecked(0x0B09);
        public static int F_LOADIMPOS = unchecked(0x0B0A);
        public static int F_SYNCHRO = unchecked(0x0B0B);
        public static int F_SENDSTAT = unchecked(0x0B0C);
        public static int F_RECORD = unchecked(0x0B0D);
        public static int F_RPCONF = unchecked(0x0B0E);
        public static int F_CONNECT = unchecked(0x0B0F);

        public  const short T_UNKNOWN = 0;
        public const short T_AMS_COMMS = 1;
        public const short T_AMS_EXM = 2;
        public const short T_AMS_IFM = 3;
        public const short T_AMS_MISC = 4;
        public const short T_VCS_FAIL = 5;
        public const short T_VCS_ERR = 6;
        public const short T_VCS_EMU = 7;

        public const short VCS_ES_BUSY = 1;
        public const short VCS_EX_LOCAL = 2;
        public const short VCS_EX_NOT_LOADED = 3;
        public const short VCS_FAILURE = 4;
        public const short VCS_INV_DATE = 8;
        public const short VCS_INV_ES_DATA = 10;
        public const short VCS_INV_EX_NAME = 11;
        public const short VCS_INV_EX_NUM = 12;
        public const short VCS_INV_EX_TIME = 13;
        public const short VCS_INV_EQUIP = 14;
        public const short VCS_INV_POS = 18;
        public const short VCS_INV_ROLE_ID = 20;
        public const short VCS_INV_STATE = 22;
        public const short VCS_INV_TIME = 25;
        public const short VCS_SUCCESS = 29;
        public const short VCS_ES_PAUSE = 34;
        public const short VCS_ES_RECORD = 35;
        public const short VCS_ES_PLAYBACK = 36;
        public const short VCS_ES_IDLE = 37;
        public const short VCS_ES_SEEK = 38;
        public const short VCS_ES_STOPPED = 39;
        public const short VCS_ES_RUN = 50;
        public const short VCS_INV_EX_TYPE = 51;
        public const short VCS_INV_FILE = 52;
        public const short VCS_INV_SVR_POS = 53;
        public const short VCS_INV_SC_TIME = 54;

        public const short AMS_WRONG_EXERCISE = 101;
        public const short AMS_UNKNOWN_ROLE = 102;
        public const short AMS_NO_ERROR = 103;
    }
}