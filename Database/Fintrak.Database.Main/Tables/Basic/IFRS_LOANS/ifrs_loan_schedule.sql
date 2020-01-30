﻿CREATE TABLE [dbo].[ifrs_loan_schedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Sequence] [int] NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[Date] [date] NOT NULL,
	[PaymentDate] [date] NULL,
	[OpeningBalance] [money] NULL DEFAULT 0,
	[AmountPrincInit] [money] NULL DEFAULT 0,
	[DailyPayment] [money] NULL DEFAULT 0,
	[DailyInt] [money] NULL DEFAULT 0,
	[DailyPrinc] [money] NULL DEFAULT 0,
	[ClosingBalance] [money] NULL DEFAULT 0,
	[AmountPrincEnd] [money] NULL DEFAULT 0,
	[AccruedInterest] [money] NULL DEFAULT 0,
	[AmortizedCost] [money] NULL DEFAULT 0,
	[NorminalRate] [float] NULL DEFAULT 0,
	[AMSequence] [int] NULL,
	[AMRefNo] [varchar](50) NULL,
	[AMDate] [date] NULL,
	[AMPaymentDate] [date] NULL,
	[AMOpeningBalance] [money] NULL DEFAULT 0,
	[AMAmountPrincInit] [money] NULL DEFAULT 0,
	[AMDailyPayment] [money] NULL DEFAULT 0,
	[AMDailyInt] [money] NULL DEFAULT 0,
	[AMDailyPrinc] [money] NULL DEFAULT 0,
	[AMClosingBalance] [money] NULL DEFAULT 0,
	[AMAmountPrincEnd] [money] NULL DEFAULT 0,
	[AMAccruedInterest] [money] NULL DEFAULT 0,
	[AMAmortizedCost] [money] NULL DEFAULT 0,
	[BalloonAmt] [money] NULL DEFAULT 0,
	[DiscountPremium] [money] NULL DEFAULT 0,
	[UnearnedFee] [money] NULL DEFAULT 0,
	[EarnedFee] [money] NULL DEFAULT 0,
	[EffectiveRate] [float] NULL DEFAULT 0,
	[NoOfPeriods] [int] NULL,
	[PostedDate] [date] NULL,
	[LastRunDate] [date] NULL,
	[CompanyCode] [varchar](50) NULL,
 CONSTRAINT [PK_ifrs_loan_schedule] PRIMARY KEY CLUSTERED 
(
	[Sequence] ASC,
	[RefNo] ASC,
	[Date] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

