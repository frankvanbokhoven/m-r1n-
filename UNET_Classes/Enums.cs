using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNET_Classes
{

    public enum UNETTheme
    {
        utDark,
        utLight,
        utBlue
    };

    public enum UNETRadioState
    {
        rsOff,  // off
        rsRx,   // Receive sound, but no transmit
        rsTx    // Receive and transmit sound
    };

    public class Enums
    {
        /// <summary>
        /// List of trainees and its index in an array
        /// </summary>
        public enum Trainees
        {
            //TraineeAA = 1,
            //TraineeBB = 2,
            //TraineeCC = 3,
            //TraineeDD = 4,
            //TraineeEE = 5,
            //TraineeFF = 6,
            //TraineeGG = 7,
            //TraineeHH = 8,
            //TraineeJJ = 9,
            //TraineeKK = 10,
            //TraineeLL = 11,
            //TraineeMM = 12,
            //TraineeNN = 13,
            //TraineePP = 14,
            //TraineeRR = 15,
            //TraineeSS = 16
            Trainee01 = 1,
            Trainee02 = 2,
            Trainee03 = 3,
            Trainee04 = 4,
            Trainee05 = 5,
            Trainee06 = 6,
            Trainee07 = 7,
            Trainee08 = 8,
            Trainee09 = 9,
            Trainee10 = 10,
            Trainee11 = 11,
            Trainee12 = 12,
            Trainee13 = 13,
            Trainee14 = 14,
            Trainee15 = 15,
            Trainee16 = 16
        }

        public enum Radios
        {
            Radio01 = 0,
            Radio02 = 1,
            Radio03 = 2,
            Radio04 = 3,
            Radio05 = 4,
            Radio06 = 5,
            Radio07 = 6,
            Radio08 = 7,
            Radio09 = 8,
            Radio10 = 9,
            Radio11 = 10,
            Radio12 = 11,
            Radio13 = 12,
            Radio14 = 13,
            Radio15 = 14,
            Radio16 = 15,
            Radio17 = 16,
            Radio18 = 17,
            Radio19 = 18,
            Radio20 = 19
        }


        public enum Exercises
        {
            Exersise01 = 0,
            Exersise02 = 1,
            Exersise03 = 2,
            Exersise04 = 3,
            Exersise05 = 4,
            Exersise06 = 5,
            Exersise07 = 6,
            Exersise08 = 7,
            Il
        }

        public enum Roles
        {
            Role01 = 0,
            Role02 = 1,
            Role03 = 2,
            Role04 = 3,
            Role05 = 4,
            Role06 = 5,
            Role07 = 6,
            Role08 = 7,
            Role09 = 8,
            Role10 = 9,
            Role11 = 10
            //todo nog 10
        }

        // id's genoemd in het SIM2VOIP document bij 4.11.2
        public enum SIM_Message_IDs
        {
            I_EC_LOAD = unchecked(0x0A00),
            A_EC_LOAD = unchecked(0x0A01),
            I_EC_START = unchecked(0x0A02),
            A_EC_START = unchecked(0x0A03),
            I_EC_STOP = unchecked(0x0A04),
            A_EC_STOP = unchecked(0x0A05),
            I_EC_PAUSE = unchecked(0x0A06),
            A_EC_PAUSE = unchecked(0x0A07),
            I_EC_PLAYBACK = unchecked(0x0A08),
            A_EC_PLAYBACK = unchecked(0x0A09),
            I_EC_SEEK = unchecked(0x0A0A),
            A_EC_SEEK = unchecked(0x0A0B),
            I_ERROR_REPORT = unchecked(0x0A0C),
            I_ES_CONFIG = unchecked(0x0A0D),
            A_ES_CONFIG = unchecked(0x0A0E),
            I_ES_DEL = unchecked(0x0A10),
            A_ES_DEL = unchecked(0x0A11),
            I_ES_END = unchecked(0x0A12),
            A_ES_END = unchecked(0x0A13),
            I_EC_RP_CONFIG = unchecked(0x0A14),
            A_EC_RP_CONFIG = unchecked(0x0A15),
            I_ES_START = unchecked(0x0A16),
            A_ES_START = unchecked(0x0A17),
            I_GET_EX_BY_ID = unchecked(0x0A18),
            A_GET_EX_BY_ID = unchecked(0x0A19),
            I_GET_EX_BY_NAME = unchecked(0x0A1A),
            A_GET_EX_BY_NAME = unchecked(0x0A1B),

            I_SC_RU_ALIVE = unchecked(0x0A1C),
            A_SC_RU_ALIVE = unchecked(0x0A1D),
            I_TIME_DATE = unchecked(0x0A20),
            A_TIME_DATE = unchecked(0x0A21),
            A_INVALID_MSG = unchecked(0x0A22),
            I_SC_RESET = unchecked(0x0A23),
            A_SC_RESET = unchecked(0x0A24),
            I_EC_EX_STATE = unchecked(0x0A25),
            A_EC_EX_STATE = unchecked(0x0A26),
            I_LOGIN = unchecked(0x0A27),
            A_LOGIN = unchecked(0x0A28),
            I_LOGOUT = unchecked(0x0A29),
            A_LOGOUT = unchecked(0x0A2A),
            I_ES_ROLE_DEF = unchecked(0x0A2B),
            A_ES_ROLE_DEF = unchecked(0x0A2C),
            I_MONITOR_START = unchecked(0x0A2D),
            A_MONITOR_START = unchecked(0x0A2E),
            I_MONITOR_END = unchecked(0x0A30),
            A_MONITOR_END = unchecked(0x0A31),
            I_IMPOSE_START = unchecked(0x0A32),
            A_IMPOSE_START = unchecked(0x0A33),
            I_IMPOSE_END = unchecked(0x0A34),
            A_IMPOSE_END = unchecked(0x0A35),
            I_EC_REPLAY = unchecked(0x0A36),
            A_EC_REPLAY = unchecked(0x0A37),
            I_EC_RESUME = unchecked(0x0A38),
            A_EC_RESUME = unchecked(0x0A39),
            I_EC_ALLOC = unchecked(0x0A3A),
            A_EC_ALLOC = unchecked(0x0A3B),

            I_CONNECTIVITY = unchecked(0x0A41),
            I_SC_SHUTDOWN = unchecked(0x0A42),
            A_SC_SHUTDOWN = unchecked(0x0A43),
            I_POWER_OFF = unchecked(0x0A44)

        }

        /// <summary>
        /// Status code values
        /// zie: 4.11.3
        /// </summary>
        public enum SIM_StatusCodes
        {
            ES_BUSY = 1,
            EX_LOCAL = 2,
            EX_NOT_LOADED = 3,
            FAILURE = 4,
            INV_DATA = 8,
            INV_ES_DATA = 10,
            INV_EX_NAME = 11,
            INV_EX_NUM = 12,
            INV_EX_TIME = 13,
            INV_EQUIP = 14,
            INV_POS = 18,
            INV_ROLE_ID = 20,
            INV_STATE = 22,
            INV_TIME = 25,
            SUCCESS = 29,
            ES_PAUSE = 34,
            ES_RECORD = 35,
            ES_PLAYBACK = 36,
            ES_IDLE = 37,
            ES_SEEK = 38,
            ES_STOPPED = 39,
            ES_RUN = 50,
            INV_EX_TYPE = 51,
            INV_FILE = 52,
            INV_SVR_POS = 53,
            INV_SC_TIME = 54
        }

        /* Enumerations */

        //##ModelId=4119F58A0388
        enum eExerciseDefinition_t
        {
            //##ModelId=4119F58A03C2
            ED_UNDEFINED,                   //No exercise defined
                                            //##ModelId=4119F58A03E0
            ED_SPECIFIED,                   //Exercise specified but not loaded into VCS
                                            //##ModelId=4119F58B0020
            ED_WAITING,                     //Appropriate VCS exercise specification and load messages queued waiting transfer* to VCS
                                            //##ModelId=4119F58B0048
            ED_LOADED,                      //Exercise loaded at VCS and ready for use
                                            //##ModelId=4119F58B0070
            ED_ALLOC,                           //Logical desk allocation and login messages queued waiting transfer* to VCS
                                                //##ModelId=4119F58B008F
            ED_READY,                           //Exercise definition ready for use but exercise not yet started
                                                //##ModelId=4119F58B00C1
            ED_ACTIVE,                      //Exercise definition in use (EC_START or EC_PLAYBACK)
                                            //##ModelId=4119F58B00E9
            ED_DELETE,                      //Exercise definition in process of being deleted from VCS
                                            //##ModelId=4119F58B0107
            ED_UNKNOWN                      //Fault has occurred
        };                                          //* or in process of being transferred

        //##ModelId=4119F58B0175
        enum eExerciseControl_t
        {
            //##ModelId=4119F58B01B1
            EC_NULL,                                        //Exercise cleared
                                                            //##ModelId=4119F58B01D9
            EC_ALLOC_PENDING_LOAD,          //Go to ED_ALLOC state once ED_LOADED state achieved
                                            //##ModelId=4119F58B0201
            EC_START_PENDING_ALLOC,         //Go to EC_START/EC_PLAYBACK state once ED_LOADED and ED_ALLOC states achieved (in order)
                                            //##ModelId=4119F58B021F
            EC_RESUME_PENDING_LOAD,         //Not used
                                            //##ModelId=4119F58B0247
            EC_PLAYBACK_PENDING_REPLAY, //Not used
                                        //##ModelId=4119F58B026F
            EC_START,                                       //Exercise started
                                                            //##ModelId=4119F58B028D
            EC_PLAYBACK,                                //Exercise playback of previous recording
                                                        //##ModelId=4119F58B02B5
            EC_PAUSE,                                       //Exercise paused
                                                            //##ModelId=4119F58B02DD
            EC_STOP,                                        //Exercise stopped
                                                            //##ModelId=4119F58B02FB
            EC_RESTART                                  //Exercise stopping but will revert to EC_ALLOC_PENDING_LOAD state once stop acknowledged
        };

        //##ModelId=4119F58B036A
       public enum eVcsLinkStatus_t
        {
            //##ModelId=4119F58B039C
            NO_CONNECT,
            //##ModelId=4119F58B03C4
            RE_CONNECT,
            //##ModelId=4119F58C0004
            FAILED_CONNECT,
            //##ModelId=4119F58C0022
            LOST_CONNECT,
            //##ModelId=4119F58C004A
            LOSTSYNC_CONNECT,
            //##ModelId=4119F58C0072
            READY_CONNECT,
            //##ModelId=4119F58C0090
            BUSY_CONNECT
        };

        //##ModelId=4119F58C00E0
     public   enum eVcsMsgStatus_t
        {
            //##ModelId=4119F58C0112
            MESSAGE_IDLE,
            //##ModelId=4119F58C013A
            MESSAGE_READY,
            //##ModelId=4119F58C0162
            MESSAGE_TRANSMITTED,
            //##ModelId=4119F58C018A
            MESSAGE_RETRANSMITTED
        };

        //##ModelId=4119F58C01DA
      public  enum eLoadType_t
        {
            //##ModelId=4119F58C0217
            EXERCISE_NULL,
            //##ModelId=4119F58C0235
            EXERCISE_NEW,
            //##ModelId=4119F58C025D
            EXERCISE_RECOVERY,
            //##ModelId=4119F58C0285
            EXERCISE_REPLAY
        };

        //##ModelId=4119F58C02CB
        enum eVcsNodeStatus_t
        {
            //##ModelId=4119F58C02FD
            NODE_DEAD,
            //##ModelId=4119F58C0325
            NODE_FAILED,
            //##ModelId=4119F58C0343
            NODE_ACTIVE
        };

        //##ModelId=4119F58C039D
        enum eVcsLoginStatus_t
        {
            //##ModelId=4119F58C03E3
            LOGIN_NOT,
            //##ModelId=4119F58D0019
            LOGIN_FAILED,
            //##ModelId=4119F58D0041
            LOGIN_REQUESTED,
            //##ModelId=4119F58D0069
            LOGIN_ACTIVE,
            //##ModelId=4119F58D0087
            LOGOUT_ACTIVE,
            //##ModelId=4119F58D00AF
            LOGIN_SUCCESS
        };

        //##ModelId=4119F58D00F6
        enum eRadioBand
        //##ModelId=4119F58D013C
        {
            eHF = 0,
            //##ModelId=4119F58D015A
            eUHF
        };

        //##ModelId=4119F58D01B4
      public  enum eErrorType
        //##ModelId=4119F58D01E6
        {
            eERROR = 0,
            //##ModelId=4119F58D020E
            eTX,
            //##ModelId=4119F58D022C
            eRX,
            //##ModelId=4119F58D025E
            eNAME
        };

    //    public enum Outcomes
    //    {
    //        false = 0,
    //        true = 1
    //    }

        //##ModelId=4119F58D02AE
       public enum eRecordPosition
        //##ModelId=4119F58D02E0
        {
            eRP_OFF = 0,
            //##ModelId=4119F58D0308
            eRP_OFF_SAVE,
            //##ModelId=4119F58D0330
            eRP_ON_MODIFY,
            //##ModelId=4119F58D034E
            eRP_ON_ALL,
            //##ModelId=4119F58D0376
            eRP_ON_PREVIOUS
        };

      //  enum { eTHREAD_EXM = 0, eTHREAD_IFM };
     //   enum { eSTT_SINGLE = 1, eSTT_MULTIPLE, eIL };
     //   enum { eBLUE = 0, eRED };
        enum todoenum {
            eROLE_MODE_INST = 0, eROLE_MODE_OP,
            eROLE_MODE_CON, eROLE_MODE_STT_IL,
            eROLE_MODE_RADOP, eROLE_MODE_ESMOP,
            eROLE_MODE_RAD, eROLE_MODE_EW
        };

        /* Structures */

        //##ModelId=4119F58D03B3
        public struct msgVcsStatus_t
        {

            char cSystemPC;
            char cDownloadPC;
            char cRecordReplayPC;
            char cArchivePC;
            char cCommsNode;//[24];
        }

//##ModelId=4119F58E001B
 struct VcsRadio_t
{
            //short		sRadioID;
            char szFrequency;//[6]
            char szStation;//[4];
    short sType;
    short sKeyNumber;
}
;

//##ModelId=4119F58E0061
 struct AmsActor_t
{

        char szPlatform;//[8]; //Role key legend Line 1		
        char szRoleName;//[8]; //Role key legend Line 2
short sKeyNumber;			//1 to 20	
} ;

//##ModelId=4119F58E00A7
struct VcsRoles_t
{

            char szPlatform;//[32];
            char szAbbrPlatform;//[8];
int nVehicleID;
short sRoleID;
int nOperatingMode;
            char szRoleName;//[10];
short sRoleType;                //0 - Trainee, 1 - Instructor
int nAssignedRadios;    //Bitwise 1= assigned, 0= not assigned (LSB = Radio[0])
int nAssignedActors;    //Bitwise 1= assigned, 0= not assigned (LSB = Actor[0])
short sPhysicalNode;
short sLogicalNode;
short sSide;                        //0=Blue, 1=Red
//eVcsLoginStatus_t eLoginStatus;
};

    /****	 Message Control  ****/
//##ModelId=4119F58E00F7
 struct vcsHeader_t
{

    short sID;              /* message identifier */
ushort usLength;        /* length of data excluding this header */
short sReserved;
};

//##ModelId=4119F58E015B
public struct queuedMsg_t
{
    //##ModelId=4119F58E01F4
   // struct queuedMsg pNextMsg;	
	//##ModelId=4119F58E0219
	short sMsgExpectedReply;
    //##ModelId=4119F58E0237
    int nMsgSize;
    //##ModelId=4119F58E0256
    vcsHeader_t MsgHeader;
    //##ModelId=4119F58E0274
    //char MsgData[TX_DATA_SEGMENT_SIZE];
};

//##ModelId=4119F58E029C
//public struct queuedMsg QueuedMsg_t;

//##ModelId=4119F58E02E2
 struct RPConfiguration_t
{
   ushort usRecordLogicalNode;
   ushort usReplayLogicalNode;
};

//##ModelId=4119F58E0332
struct ReceptionMap_t
{
   ushort usLogicalNodeNo;
   ushort usPercentageReception;
};

        public int NO_OF_COMMS_VEHICLES = 0; //todo: juist getal invoegen

        public struct MaskState_E
        {

        }
        //##ModelId=4119F58E0382
        public struct RefConnectivity_t
        {

            MaskState_E ownPlatformUHFMast;
            MaskState_E ownPlatformHFMast;
           // long friendlyVehicles[NO_OF_COMMS_VEHICLES];
           // MaskState_E availableHFComms[NO_OF_COMMS_VEHICLES];
           // MaskState_E availableUHFComms[NO_OF_COMMS_VEHICLES];
        }
        //##ModelId=4119F58E03C8
     public   struct Login_t
        {

            short sExerciseNumber;
            char cRoleName;//[10];
            short sLogicalNode;
        }

		/****  Exercise Management ****/
//##ModelId=4119F58F0030
struct VcsExercise_t
    {
  short sVcsExerciseNumber;

//eExerciseDefinition_t eED_State;
//eExerciseControl_t eEC_State;
//eLoadType_t eLoadType;
bool bRestart;
byte cScenarioState;


//bool bMonitorActiveSupervisor[MAX_NUM_INSTRUCTORS];
//short sMonitorActiveTrainee[MAX_NUM_INSTRUCTORS];               //1 to TOTAL_NUM_TRAINEES

        char szExerciseSpecificationName;//[25];
        char szUniqueExerciseName;//[25];                  //First 10 chars appear in 'exercise' keyframe
        char szRecordFileName;//[125];
        char szLogFileName;//[128];
        char szRecordFileIndex;//[15]; // ddmmyyyyhhnnss

        char szExerciseMode;//[11];

double dScenarioTime;
double dExerciseTime;
int nTickMultiplier;

bool bIndividualTraining;

short sMasterInstPhysicalNode;  //1 to 4 = Instructor 1 to Instructor 4

short sRoleCount;       //Maximum value MAX_NUM_ROLES
short sRadioCount;  //Maximum value MAX_NUM_RADIOS
short sActorCount;  //Maximum value MAX_NUM_ACTORS

//VcsRadio_t Radio[MAX_NUM_RADIOS];
//VcsRoles_t Roles[MAX_NUM_NODES];
//AmsActor_t Actor[MAX_NUM_ACTORS];

/* Record/Replay Control */
short sRPCount;                                 //physical record/replay node count
//short sRPNode[MAX_NUM_NODES];   //physical record/replay node

bool sRPPreviousValid;
short sRPCountPrevious;
        short sRPNodePrevious;//[MAX_NUM_NODES];

short sShortTermRecordControl;
short sLongTermRecordControl;

        /* Connectivity Control */
        RefConnectivity_t RefConnectivity;//[MAX_NUM_DESKS];

/* Output Message Control */
//HANDLE hQueueMutex;
bool bStoppingExercise;
bool bStopAllExerciseProcessing;
short nMsgCnt;
//eVcsMsgStatus_t eMsgStatus;
//QueuedMsg_t* pMsgQueue;

};

		/****  Non-Exercise Msg Management ****/
//##ModelId=4119F58F0076
struct VcsGeneral_t
{

 //   HANDLE hQueueMutex;
//bool bStopAllExerciseProcessing;
//short nMsgCnt;
//eVcsMsgStatus_t eMsgStatus;
//QueuedMsg_t pMsgQueue;
};

		/****  Interface Management ****/
//##ModelId=4119F58F00C6
struct VcsInterface_t
    {

   // eVcsLinkStatus_t eVCS_LinkState;
bool bVcsControlPcStatus;
//byte cVcsNodeStatus[MAX_NUM_NODES];                                                     //uses CScenarioComponent::enum ScenarioState		
//byte cVcsInstructorNodeExerciseStatus[MAX_NUM_INSTRUCTORS]; //bitwise state 1 - operational,
                                                            //							0 - fault
                                                            //			where lsb = Exercise1, msb = Exercise 8
//HANDLE hTransmitMutex;
int nTransmittedVICExerciseNo;
bool bMessagePendingReconnect;
int nTotalMessagesTxThisIteration;
long lMessageIndex;
//QueuedMsg_t msgRU_ALIVE;

} ;

		/****  Desk Management ****/
//##ModelId=4119F58F0116
struct VcsNode_t
{
   // eVcsNodeStatus_t eNodeStatus;
}
    }
}
