USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndCheckingIssues_Par_Pkt_Sec]    Script Date: 05/07/2026 2:26:42 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndCheckingIssues_Par_Pkt_Sec] ON [dbo].[tblGrading_RndCheckingIssues]
(
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndCheckingReturns_Par_Pkt_Sec]    Script Date: 05/07/2026 2:26:58 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndCheckingReturns_Par_Pkt_Sec] ON [dbo].[tblGrading_RndCheckingReturns]
(
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndCheckingTypes_Par_Pkt_Sec]    Script Date: 05/07/2026 2:27:17 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndCheckingTypes_Par_Pkt_Sec] ON [dbo].[tblGrading_RndCheckingTypes]
(
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndPacket_Par_Pkt]    Script Date: 05/07/2026 2:27:42 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndPacket_Par_Pkt] ON [dbo].[tblGrading_RndPacket]
(
	[ParNo] ASC,
	[PktNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

/****** Object:  Index [idx_tblGrading_RndPackingListM_ID]    Script Date: 05/07/2026 2:28:20 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndPackingListM_ID] ON [dbo].[tblGrading_RndPackingListM]
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO




USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndSizingDetails_Dep_Par_Pkt_Sec]    Script Date: 05/07/2026 2:29:35 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndSizingDetails_Dep_Par_Pkt_Sec] ON [dbo].[tblGrading_RndSizingDetails]
(
	[Department] ASC,
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndSizingIssues_Par_Pkt_Sec]    Script Date: 05/07/2026 2:29:59 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndSizingIssues_Par_Pkt_Sec] ON [dbo].[tblGrading_RndSizingIssues]
(
	[Department] ASC,
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndSizingPacket_Par_Pkt]    Script Date: 05/07/2026 2:30:26 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndSizingPacket_Par_Pkt] ON [dbo].[tblGrading_RndSizingPacket]
(
	[Department] ASC,
	[ParNo] ASC,
	[PktNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndSizingReturns_Par_Pkt_Sec]    Script Date: 05/07/2026 2:30:47 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndSizingReturns_Par_Pkt_Sec] ON [dbo].[tblGrading_RndSizingReturns]
(
	[Department] ASC,
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


USE [DiaStock]
GO

SET ANSI_PADDING ON
GO

/****** Object:  Index [idx_tblGrading_RndSizingTypes_Par_Pkt]    Script Date: 05/07/2026 2:31:31 PM ******/
CREATE NONCLUSTERED INDEX [idx_tblGrading_RndSizingTypes_Par_Pkt] ON [dbo].[tblGrading_RndSizingTypes]
(
	[Department] ASC,
	[ParNo] ASC,
	[PktNo] ASC,
	[Sec] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO


