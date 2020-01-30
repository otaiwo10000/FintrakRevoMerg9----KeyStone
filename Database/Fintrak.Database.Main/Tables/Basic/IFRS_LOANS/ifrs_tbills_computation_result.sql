﻿CREATE TABLE [dbo].[ifrs_tbills_computation_result](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[Description] [varchar](450) NULL,
	[DealTypeId] [varchar](250) NULL,
	[ValueDate] [date] NULL,
	[MaturityDate] [date] NULL,
	[TotalTenor] [int] NULL,
	[UsedDays] [int] NULL,
	[Daystomaturity] [int] NULL,
	[Rate] [decimal](38, 4) NULL,
	[FaceValue] [money] NULL,
	[CleanPrice] [money] NULL,
	[CurrentMarketYield] [decimal](38, 4) NULL,
	[ComputedMarketPrice] [money] NULL,
	[IntrestReceivable] [money] NULL,
	[AmortizedCost] [money] NULL,
	[FairValueGain] [money] NULL,
	[Classification] [varchar](50) NULL,
	[Year] [int] NULL,
	[Period] [int] NULL,
	[RunDate] [date] NULL,
	[FairValueBasis] [int] NULL,
	[Currency] [varchar](50) NULL,
	[CompanyCode] [varchar](50) NULL, 
    CONSTRAINT [PK_ifrs_tbills_computation_result] PRIMARY KEY ([id])
) ON [PRIMARY]