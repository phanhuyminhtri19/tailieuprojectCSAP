USE [UniHostelDB]
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (2, N'Host')
INSERT [dbo].[Roles] ([RoleID], [RoleName]) VALUES (3, N'Renter')
SET IDENTITY_INSERT [dbo].[Roles] OFF
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'1', N'dinhphu', N'123', 1, 2)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'2', N'cachua', N'123', 1, 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'201811051816408999', N'caphao', N'123', 1, 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'201811051823451190', N'catim', N'123', 1, 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'201811051827008444', N'caphe', N'123', 1, 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'201811051832456102', N'cari', N'123', 1, 3)
INSERT [dbo].[Users] ([ID], [Username], [Password], [isActive], [RoleID]) VALUES (N'3', N'admin', N'123', 1, 1)
INSERT [dbo].[Hosts] ([ID], [FullName], [Address], [Phone], [Mail], [NumOfRoom], [Description], [isActive]) VALUES (N'1', N'Nguyen Dinh Phu', N'Binh Duong', N'01699327997', N'dinhphu@gmail.com', 12, NULL, 1)
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'10', N'PHÒNG TRỌ CAO CẤP  NGUYỄN HUỆ', N'/asset/imgRoommanager/room2.jpg', 50, 9300000, N'👉Phòng riêng lối đi riêng biệt có gác nhà vệ sinh riêng,bếp, khu vực trung tâm thành phố<br/> 👉Cách phố đi bộ Nguyễn Huệ 350m xung quanh là khu biệt thự đường 10 yên tĩnh an toàn. <br/>👉Tọa lạc ngay tâm điểm của Thảo Điền - nơi lưu trú của các chuyên gia người nước ngoài,... <br/>👉Nằm trong hẻm rộng rãi xe ô tô ra vào thoải mái  ra vào và đi lại', 0, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'11', N'PHÒNG TRỌ CAO CẤP PHAN VĂN TRỊ', N'/asset/imgRoommanager/room3.jpg', 35, 6500000, N'👉Phòng đầy đủ tiện nghi: Phòng mới sạch sẽ, có cửa sổ. Có ban công, điều hòa, tủ lạnh, máy giặt,..<br/>
    👉Tiện ích: Gần chợ, phòng Gym, gần siêu thị Lotte Gò Vấp <br/> 👉Giờ giấc tự do có tích hợp khóa vân tay <br/> 👉Tầm nhìn đều có hướng nhìn ra sông Sài Gòn thơ mộng và khuôn viên cây xanh thoáng mát tại khuôn viên.', 0, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'12', N'PHÒNG TRỌ CAO CẤP PHAN VĂN TRỊ 2', N'/asset/imgRoommanager/room4.jpg', 70, 1E+07, N'👉Phòng cực đẹp, cực rộng, full nội thất. Có thể ở từ 5-7 người.<br/>👉Thiết kế tòa nhà dạng chung cư mini,Phòng sạch sẽ, sang trọng. thoáng mát
<br/>👉Có thang máy tốc độ cao và thang bộ rộng rãi.<br/>👉Nằm trong khu cao cấp Thảo Điền nên rất an ninh, yên tĩnh và thoáng mát.<br/> 👉Có nhiều dịch vụ tiện tích,..', 0, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'13', N'PHÒNG TRỌ CAO CẤP QUẬN LÊ ĐỨC THỌ', N'/asset/imgRoommanager/room5.jpg', 75, 1.1E+07, N'👉Phòng thiết kế cực kỳ cao cấp sang chảnh với sàn nhà lát gạch sáng bóng sạch sẽ cùng các trang thiết bị nội thất thông minh cực kỳ thú vị.
    <br/>👉Tiện nghi trong phòng đầy đủ, Giường nệm cho bạn những giấc ngủ thoải mái<br/>👉Thuận tiện di chuyển ra các quận trung tâm Q.1, Q.3, Q.7, Q. Bình Thạnh.² <br/>👉Bảo vệ 24/24', 0, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'14', N'PHÒNG TRỌ CAO CẤP QUẬN GÒ VẤP', N'/asset/imgRoommanager/room6.jpg', 30, 5000000, N'👉Tiện đi qua bình thạnh, phú nhuận ở được 4-6 người phù hợp với các bạn sinh viên hoặc gia đình
Wifi, cáp quang mạnh Giờ giấc thoài mái.<br/> 👉Nhà wc riêng , kệ bếp nấu ăn.<br/> 👉Hồ bơi ngoài trời và cây xanh ở các cụm căn hộ (miễn phí).
 Khu vui chơi trẻ em ngoài trời (miễn phí).<br/>👉Hệ thống trường học liên cấp từ mầm non đến cấp 2', 0, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'15', N'PHÒNG TRỌ CAO CẤP QUẬN GÒ VẤP', N'/asset/imgRoommanager/room7.jpg', 30, 5000000, N' 👉Căn hộ mới xây còn zin, đầu tháng 9 sẽ ở được. Với diện tích 50 m², 1 phòng ngủ và 1 phòng khách riêng biệt, phòng thiết kế đa dạng<br/> 👉Khu vực nằm trung tâm Gò Vấp, giáp với các đường Quang Trung, Lê Đức Thọ, Nguyễn Văn Lượng, gần các trường Đại học<br/> 👉Trung tâm thương mại mua sắm song song cách dịch vụ tiện ích cho gia đình', 1, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'16', N'CĂN HỘ CHUNG CƯ TROPIC GARDEN', N'/asset/imgRoommanager/room6.jpg', 90, 2.9E+07, N'👉Căn hộ gồm 3 phòng ngủ, 2 nhà vệ sinh, 1 nhà bếp, 1 phòng khách và 1 sân phơi, ban công cửa sổ hướng tòa nhà LANMARK.<br/> 👉Tọa lạc ngay tâm điểm của Thảo Điền - nơi lưu trú của các chuyên gia người nước ngoài, nơi an cư yêu thích của nhiều doanh nhân thành đạt, các cán bộ hưu trí và các nhà ngoại giao. Nằm giữa trung tâm Thảo Điền, ...', 1, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'25', N'CĂN HỘ CHUNG CƯ QUẬN PHÚ NHUẬN', N'/asset/imgRoommanager/room8.jpg', 70, 9000000, N'👉Xung quanh tòa nhà còn rất nhiều tiện ích như: gần chợ, quán ăn, quán cà phê và nhiều dịch vụ khác.<br/>👉Rất thuận lợi cho những bạn sinh viên các trường ĐH Hutech (Điện Biên Phủ), ĐH Ngoại Thương <br/>👉Nằm trong hẻm yên tĩnh thoáng mát, an ninh, và cách đại lộ Phạm Văn Đồng chưa đầy 7p đi xe và sân bay 11p đi xe, thuận tiện cho việc đi lại giữa các quận như Quận 1, ...', 1, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'9', N'PHÒNG TRỌ CAO CẤP QUẬN GÒ VẤP', N'/asset/imgRoommanager/room1.jpg', 40, 7300000, N'👉Nội thất sang trọng, phòng sạch sẽ, mới 100% Toilet riêng từng phòng<br/> 👉Trang bị sẵn giường, nệm, gối đầu, mền cao cấp. 
        Wifi tốc độ cao miễn phí<br/>👉Khu vực an ninh tuyệt đối, Có camera an ninh 24/24, cửa khóa vân tay<br/>👉Nằm gần các trung tâm thương mại lớn như Parkson,...', 1, 1, N'1')
INSERT [dbo].[Rooms] ([ID], [Name], [Image], [Square], [Price], [Description], [isAvailable], [isActive], [HostID]) VALUES (N'Room 1', N'Phòng trọ Bình Dương', N'/asset/imgRoommanager/room1.jpg', 12, 1E+07, N'👉Tiện đi qua bình thạnh, phú nhuận ở được 4-6 người phù hợp với các bạn sinh viên hoặc gia đình
Wifi, cáp quang mạnh Giờ giấc thoài mái.<br/> 👉Nhà wc riêng , kệ bếp nấu ăn.<br/> 👉Hồ bơi ngoài trời và cây xanh ở các cụm căn hộ (miễn phí).
 Khu vui chơi trẻ em ngoài trời (miễn phí).<br/>👉Hệ thống trường học liên cấp từ mầm non đến cấp 2', 1, 1, N'1')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'2', N'Ca Chua Nguyen Thi', CAST(N'2018-09-28 00:00:00.000' AS DateTime), NULL, CAST(N'1998-05-09' AS Date), N'cachua@gmail.com', N'Ha Noi', N'456', NULL, N'11')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'201811051816408999', N'Nguyen Thi Ca Phao', CAST(N'2018-11-05 18:16:40.940' AS DateTime), NULL, NULL, N'dinhphu951998@gmail.com', N'Binh Duong', N'123', NULL, N'10')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'201811051823451190', N'Tran Thi Ca Tim', CAST(N'2018-11-05 18:23:45.217' AS DateTime), NULL, NULL, N'catim@gmail.com', N'Tây Ninh', N'123', NULL, N'12')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'201811051827008444', N'Nguyễn Cà Phê', CAST(N'2018-11-05 18:27:00.870' AS DateTime), NULL, NULL, N'caphe@gmail.com', N'Long An', N'123', NULL, N'14')
INSERT [dbo].[Renters] ([ID], [FullName], [StartDate], [EndDate], [BirthDate], [Mail], [HomeTown], [Phone], [Description], [RoomID]) VALUES (N'201811051832456102', N'Nguyễn Thị Cà Ri', CAST(N'2018-11-05 18:32:45.710' AS DateTime), NULL, NULL, N'cari@gmail.com', N'Bình Thuận', N'123', NULL, N'13')
INSERT [dbo].[Bills] ([ID], [Time], [OtherFee], [Total], [isPaid], [Description], [RoomID], [RenterID]) VALUES (N'201810301011068471', CAST(N'2018-10-30 10:11:06.847' AS DateTime), NULL, 1.29565E+07, 1, NULL, N'Room 1', N'2')
INSERT [dbo].[CompulsoryServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162134469962', N'Điện', 2500, N'KWh', NULL, 1, N'1')
INSERT [dbo].[CompulsoryServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162135048355', N'Nước', 13000, N'Khối (m3)', NULL, 1, N'1')
INSERT [dbo].[AdvancedServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162135442138', N'Wifi', 50000, N'Phòng', NULL, 1, N'1')
INSERT [dbo].[AdvancedServices] ([ID], [Name], [Price], [Unit], [Description], [isActive], [HostID]) VALUES (N'201810162135566053', N'Gửi xe', 60000, N'Chiếc', NULL, 1, N'1')
SET IDENTITY_INSERT [dbo].[BillAdvancedServiceDetails] ON 

INSERT [dbo].[BillAdvancedServiceDetails] ([ID], [AdvancedServiceID], [BillID], [Time], [Quantity], [Amount], [Description]) VALUES (11, N'201810162135442138', N'201810301011068471', CAST(N'2018-10-30 03:11:06.367' AS DateTime), 21, 1050000, NULL)
SET IDENTITY_INSERT [dbo].[BillAdvancedServiceDetails] OFF
SET IDENTITY_INSERT [dbo].[BillCompulsoryServiceDetails] ON 

INSERT [dbo].[BillCompulsoryServiceDetails] ([ID], [BillID], [CompulsoryServiceID], [Time], [PreNum], [CurNum], [Amount], [Description]) VALUES (13, N'201810301011068471', N'201810162134469962', CAST(N'2018-10-30 03:11:06.367' AS DateTime), 0, 123, 307500, NULL)
INSERT [dbo].[BillCompulsoryServiceDetails] ([ID], [BillID], [CompulsoryServiceID], [Time], [PreNum], [CurNum], [Amount], [Description]) VALUES (14, N'201810301011068471', N'201810162135048355', CAST(N'2018-10-30 03:11:06.367' AS DateTime), 0, 123, 1599000, NULL)
SET IDENTITY_INSERT [dbo].[BillCompulsoryServiceDetails] OFF
