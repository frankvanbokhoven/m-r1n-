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

        // id's genoemd in het SIM2UNET document bij 4.11.2
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
    }
}
