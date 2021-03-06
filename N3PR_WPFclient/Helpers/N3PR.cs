﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N3PR_WPFclient.Helpers
{
    public static class N3PR
    {
        public static string DATA_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public static int MAX_RESEND_TRIALS = 5;

        public static string DATE = "DATE";
        public static string REG_NAME = "REG_NAME";
        public static string BVAL = "B_VAL";
        public static string IVAL = "I_VAL";
        public static string UIVAL = "UI_VAL";

        public static List<string> REG_NAMES = new List<string> {
        "STE_PTA",
        "STE_PTE",
        "STE_PTR",
        "STE_ASP",
        "STE_IPS",
        "STE_OPS",
        "STE_SPS",
        "STE_POW",
        "STE_ADB",
        "STE_ADC",
        "STE_BST",
        "STE_BSP",
        "STW_WPS",
        "STW_WDV",
        "STE_FCL",
        "STE_SDV",
        "STE_SBV",
        "STE_TER",
        "STE_TSR",
        "STE_PSR",
        "STE_TLP",
        "STE_SDS",
        "STE_SD1",
        "STE_SD2",
        "STE_SD3",
        "STE_SD4",
        "STE_SD5",
        "STE_SD6",
        "STE_SD7",
        "STE_SD8",
        "STE_RFV",
        "STA_PTS",
        "STA_RTR",
        "STA_DPS",
        "STA_SPT",
        "STA_LCK",
        "STA_PP1",
        "STA_PP2",
        "STA_PP3",
        "STA_PP4",
        "STA_PP5",
        "STA_PP6",
        "STA_PP7",
        "STA_PP8",
        "STA_PMP",
        "STA_PSP",
        "STA_MF1",
        "STA_MF2",
        "STA_MF3",
        "STA_MF4",
        "STA_MF5",
        "STA_MF6",
        "STA_RSV",
        "STA_FLM",
        "STA_TLP",
        "STA_RFV",
        "STA_FIN",
        "STA_FOU",
        "STB_PTS",
        "STB_RTR",
        "STB_DPS",
        "STB_SPT",
        "STB_LCK",
        "STB_PP1",
        "STB_PP2",
        "STB_PP3",
        "STB_PP4",
        "STB_PP5",
        "STB_PP6",
        "STB_PP7",
        "STB_PP8",
        "STB_PMP",
        "STB_PSP",
        "STB_MF1",
        "STB_MF2",
        "STB_MF3",
        "STB_MF4",
        "STB_MF5",
        "STB_MF6",
        "STB_RSV",
        "STB_FLM",
        "STB_TLP",
        "STB_RFV",
        "STB_FIN",
        "STB_FOU",
        "STC_PTS",
        "STC_RTR",
        "STC_DPS",
        "STC_SPT",
        "STC_LCK",
        "STC_PP1",
        "STC_PP2",
        "STC_PP3",
        "STC_PP4",
        "STC_PP5",
        "STC_PP6",
        "STC_PMP",
        "STC_PSP",
        "STC_MF1",
        "STC_MF2",
        "STC_MF3",
        "STC_MF4",
        "STC_MF5",
        "STC_MF6",
        "STC_RSV",
        "STC_FLM",
        "STC_TLP",
        "STC_RFV",
        "STC_FIN",
        "STC_FOU",
        "STD_PTS",
        "STD_RTR",
        "STD_DPS",
        "STD_SPT",
        "STD_LCK",
        "STD_PP1",
        "STD_PP2",
        "STD_PP3",
        "STD_PP4",
        "STD_PP5",
        "STD_PP6",
        "STD_PMP",
        "STD_PSP",
        "STD_MF1",
        "STD_MF2",
        "STD_MF3",
        "STD_MF4",
        "STD_MF5",
        "STD_MF6",
        "STD_RSV",
        "STD_FLM",
        "STD_TLP",
        "STD_RFV",
        "STD_FIN",
        "STD_FOU",
        "STH_TSH",
        "STH_TRH",
        "STH_TAH",
        "STH_FCV",
        "STH_ACT",
        "STH_DMN",
        "STH_STA",
        "SH1_ACT",
        "SH1_DMN",
        "SH2_ACT",
        "SH2_DMN",
        "SH3_ACT",
        "SH3_DMN",
        "SH4_ACT",
        "SH4_DMN",
        "SH5_ACT",
        "SH5_DMN",
        "SH6_ACT",
        "SH6_DMN",
        "STX_AIN",
        "STX_AOU",
        "SYS_STA"
        };
        public static List<ushort> REG_ADDRESS = new List<ushort> {
            100,
            101,
            102,
            103,
            104,
            105,
            106,
            107,
            100,
            108,
            101,
            109,
            102,
            103,
            104,
            105,
            106,
            110,
            111,
            112,
            113,
            114,
            107,
            108,
            109,
            110,
            111,
            112,
            113,
            114,
            115,
            115,
            116,
            117,
            118,
            116,
            117,
            118,
            119,
            120,
            121,
            122,
            123,
            124,
            119,
            120,
            121,
            122,
            123,
            124,
            125,
            126,
            125,
            127,
            128,
            126,
            129,
            130,
            131,
            132,
            133,
            134,
            127,
            128,
            129,
            130,
            131,
            132,
            133,
            134,
            135,
            135,
            136,
            137,
            138,
            139,
            140,
            141,
            142,
            136,
            143,
            144,
            137,
            145,
            146,
            147,
            148,
            149,
            150,
            138,
            139,
            140,
            141,
            142,
            143,
            144,
            151,
            152,
            153,
            154,
            155,
            156,
            157,
            158,
            145,
            159,
            160,
            146,
            161,
            162,
            163,
            164,
            165,
            166,
            147,
            148,
            149,
            150,
            151,
            152,
            153,
            167,
            168,
            169,
            170,
            171,
            172,
            173,
            174,
            154,
            175,
            176,
            155,
            177,
            178,
            179,
            180,
            181,
            156,
            182,
            183,
            184,
            185,
            186,
            187,
            188,
            189,
            190,
            191,
            192,
            193,
            194,
            195,
            196,
            197,
            198,
            199
        };
        public static List<string> REG_FUNCTION_CODES = new List<string> {
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x02",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04"
        };
        public static List<string> ALARM_NAMES = new List<string> {
            "WNS_PBE",
            "WNS_PAE",
            "WNS_DRN",
            "WNS_FPD",
            "ALS_FPD",
            "WNS_ADB",
            "WNS_BST",
            "WNS_PSE",
            "ALS_PSE",
            "WNS_BWR",
            "WNF_HEC",
            "WBV_OPN",
            "WBV_NCL",
            "ABV_NOP",
            "A09_COM",
            "A09_BRK",
            "E09_COM",
            "W09_SL0",
            "W09_SL1",
            "W09_SL2",
            "W09_SL3",
            "W09_SL4",
            "W09_SL5",
            "W09_SL6",
            "W09_SL7",
            "W09_SL8",
            "W09_SL9",
            "W09_TSE",
            "A09_TAE",
            "A09_TSE",
            "W09_TRE",
            "A09_PIE",
            "A09_POE",
            "A09_LTE",
            "A09_PSE",
            "W09_PB1",
            "W09_PB2",
            "W09_PB3",
            "W09_PAE",
            "A09_PMP",
            "A10_COM",
            "A10_BRK",
            "E10_COM",
            "W10_SL0",
            "W10_SL1",
            "W10_SL2",
            "W10_SL3",
            "W10_SL4",
            "W10_SL5",
            "W10_SL6",
            "W10_SL7",
            "W10_SL8",
            "W10_SL9",
            "A10_PSE",
            "W10_PB1",
            "W10_PB2",
            "W10_PB3",
            "W10_PAE",
            "A10_PMP",
            "A11_COM",
            "A11_BRK",
            "E11_COM",
            "W11_SL0",
            "W11_SL1",
            "W11_SL2",
            "W11_SL3",
            "W11_SL4",
            "W11_SL5",
            "W11_SL6",
            "W11_SL7",
            "W11_SL8",
            "W11_SL9",
            "A11_PSE",
            "W11_PB1",
            "W11_PB2",
            "W11_PB3",
            "W11_PAE",
            "A11_PMP",
            "A12_COM",
            "A12_BRK",
            "E12_COM",
            "W12_SL0",
            "W12_SL1",
            "W12_SL2",
            "W12_SL3",
            "W12_SL4",
            "W12_SL5",
            "W12_SL6",
            "W12_SL7",
            "W12_SL8",
            "W12_SL9",
            "A12_PSE",
            "W12_PB1",
            "W12_PB2",
            "W12_PB3",
            "W12_PAE",
            "A12_PMP",
            "A01_COM",
            "E01_COM",
            "A01_LWL",
            "A01_LWS",
            "A01_LWR",
            "W01_REF",
            "W01_TSA",
            "W01_LPD",
            "A01_LPD",
            "A01_LSP",
            "W01_HPS",
            "W01_FPD",
            "A01_FPD",
            "W01_PSR",
            "A01_TSA",
            "W01_TRA",
            "A01_PSA",
            "A01_LTA",
            "A01_FIA",
            "A01_FOA",
            "A01_FMT",
            "A01_PP1",
            "A01_PP2",
            "A01_PP3",
            "A01_PP4",
            "A01_G11",
            "A01_G12",
            "A01_G13",
            "W01_G11",
            "W01_G12",
            "A01_G21",
            "A01_G22",
            "A01_G23",
            "W01_G21",
            "W01_G22",
            "A01_G31",
            "A01_G32",
            "A01_G33",
            "W01_G31",
            "W01_G32",
            "A01_G41",
            "A01_G42",
            "A01_G43",
            "W01_G41",
            "W01_G42",
            "A02_COM",
            "E02_COM",
            "A02_LWL",
            "A02_LWS",
            "A02_LWR",
            "W02_REF",
            "W02_TSB",
            "W02_LPD",
            "A02_LPD",
            "A02_LSP",
            "W02_HPS",
            "W02_FPD",
            "A02_FPD",
            "W02_PSR",
            "A02_TSB",
            "W02_TRB",
            "A02_PSB",
            "A02_LTB",
            "A02_FIB",
            "A02_FOB",
            "A02_FMT",
            "A02_PP1",
            "A02_PP2",
            "A02_PP3",
            "A02_PP4",
            "A02_G11",
            "A02_G12",
            "A02_G13",
            "W02_G11",
            "W02_G12",
            "A02_G21",
            "A02_G22",
            "A02_G23",
            "W02_G21",
            "W02_G22",
            "A02_G31",
            "A02_G32",
            "A02_G33",
            "W02_G31",
            "W02_G32",
            "A02_G41",
            "A02_G42",
            "A02_G43",
            "W02_G41",
            "W02_G42",
            "A03_COM",
            "A03_LWL",
            "A03_LWS",
            "A03_LWR",
            "W03_REF",
            "W03_TSC",
            "W03_LPD",
            "A03_LPD",
            "A03_LSP",
            "W03_HPS",
            "W03_FPD",
            "A03_FPD",
            "W03_PSR",
            "A03_TSC",
            "W03_TRC",
            "A03_PSC",
            "A03_LTC",
            "A03_FIC",
            "A03_FOC",
            "A03_FMT",
            "A03_PP1",
            "A03_PP2",
            "A03_PP3",
            "A03_PP4",
            "A05_COM",
            "A05_LWL",
            "A05_LWS",
            "A05_LWR",
            "W05_REF",
            "A05_LTE",
            "A05_LED",
            "A05_HED",
            "W05_PSR",
            "W05_RTS",
            "W05_TSR",
            "A05_TER",
            "A05_TSR",
            "A05_PSR",
            "A05_LTR",
            "A05_PP1",
            "A05_PP2",
            "A05_PP3",
            "A05_PP4",
            "A05_PP5",
            "A05_PP6",
            "A05_PP7",
            "A05_PP8",
            "A07_COM",
            "E07_COM",
            "W07_TSH",
            "A07_TSH",
            "A07_TRH",
            "A07_TAH",
            "A07_OFF",
            "A07_RMT",
            "A07_C11",
            "A07_C12",
            "A07_C13",
            "A07_C14",
            "A07_C15",
            "A07_C16",
            "A07_C21",
            "A07_C22",
            "A07_C23",
            "A07_C24",
            "A07_C25",
            "A07_C26",
            "A07_C31",
            "A07_C32",
            "A07_C33",
            "A07_C34",
            "A07_C35",
            "A07_C36",
            "A07_C41",
            "A07_C42",
            "A07_C43",
            "A07_C44",
            "A07_C45",
            "A07_C46",
            "PLC_CM1",
            "PLC_CM2",
            "PLC_CM3",
            "PLC_RED",
            "PLC_YLW"   
        };
        public static List<ushort> ALARM_ADDRESS = new List<ushort> {
            550,
            551,
            552,
            553,
            554,
            555,
            556,
            557,
            558,
            559,
            560,
            561,
            562,
            563,
            564,
            565,
            275,
            276,
            277,
            278,
            279,
            280,
            281,
            282,
            283,
            284,
            285,
            566,
            567,
            568,
            569,
            570,
            571,
            572,
            573,
            574,
            575,
            576,
            577,
            578,
            579,
            580,
            286,
            287,
            288,
            289,
            290,
            291,
            292,
            293,
            294,
            295,
            296,
            581,
            582,
            583,
            584,
            585,
            586,
            587,
            588,
            297,
            298,
            299,
            300,
            301,
            302,
            303,
            304,
            305,
            306,
            307,
            589,
            590,
            591,
            592,
            593,
            594,
            595,
            596,
            308,
            309,
            310,
            311,
            312,
            313,
            314,
            315,
            316,
            317,
            318,
            597,
            598,
            599,
            600,
            601,
            602,
            603,
            319,
            604,
            605,
            606,
            607,
            608,
            609,
            610,
            611,
            612,
            613,
            614,
            615,
            616,
            617,
            618,
            619,
            620,
            621,
            624,
            626,
            627,
            628,
            629,
            320,
            321,
            322,
            323,
            324,
            325,
            326,
            327,
            328,
            329,
            330,
            331,
            332,
            333,
            334,
            335,
            336,
            337,
            338,
            339,
            634,
            340,
            635,
            636,
            637,
            638,
            639,
            640,
            641,
            642,
            643,
            644,
            645,
            646,
            647,
            648,
            649,
            650,
            651,
            652,
            655,
            657,
            658,
            659,
            660,
            341,
            342,
            343,
            344,
            345,
            346,
            347,
            348,
            349,
            350,
            351,
            352,
            353,
            354,
            355,
            356,
            357,
            358,
            359,
            360,
            665,
            666,
            667,
            668,
            669,
            670,
            671,
            672,
            673,
            674,
            675,
            676,
            677,
            678,
            679,
            680,
            681,
            682,
            683,
            686,
            688,
            689,
            690,
            691,
            696,
            697,
            698,
            699,
            700,
            701,
            702,
            703,
            704,
            705,
            706,
            707,
            708,
            709,
            710,
            711,
            712,
            713,
            714,
            715,
            716,
            717,
            718,
            719,
            361,
            720,
            721,
            722,
            723,
            362,
            363,
            364,
            365,
            366,
            367,
            368,
            369,
            370,
            371,
            372,
            373,
            374,
            375,
            376,
            377,
            378,
            379,
            380,
            381,
            382,
            383,
            384,
            385,
            386,
            387,
            724,
            725,
            726,
            727,
            728
        };
        public static List<string> ALARM_FUNCTION_CODES = new List<string> {
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x04",
            "0x02",
            "0x02",
            "0x02",
            "0x02",
            "0x02"
        };
    }
}
