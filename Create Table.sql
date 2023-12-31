USE [QLBANDT]
GO
/****** Object:  Table [dbo].[ChiTietDH]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietDH](
	[Soluong] [int] NOT NULL,
	[Dongia] [int] NOT NULL,
	[ThanhTien] [int] NOT NULL,
	[MaDH] [int] NOT NULL,
	[MaSP] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DonHang]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang](
	[MaDH] [int] IDENTITY(1,1) NOT NULL,
	[NgayDH] [date] NOT NULL,
	[NguoiNhan] [nvarchar](50) NOT NULL,
	[DiaChiNhan] [nvarchar](50) NOT NULL,
	[HTthanhtoan] [nvarchar](50) NULL,
	[NgayGH] [date] NULL,
	[Trigia] [int] NULL,
	[Sodienthoainhan] [nvarchar](11) NOT NULL,
	[MaUser] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaDH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mau]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mau](
	[Tenmau] [nvarchar](20) NOT NULL,
	[Mamau] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Mamau] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mau_sp]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mau_sp](
	[Mamau] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Mamau] ASC,
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[MaOr] [int] IDENTITY(1,1) NOT NULL,
	[DateOr] [date] NULL,
	[SDT] [nvarchar](11) NULL,
	[TenNgNhan] [nvarchar](30) NULL,
	[DiaChiNhan] [nvarchar](30) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaOr] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrdersDetail]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrdersDetail](
	[MaOrD] [int] IDENTITY(1,1) NOT NULL,
	[SoLuong] [int] NULL,
	[ThanhTien] [int] NULL,
	[MaSP] [int] NULL,
	[MaOr] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[MaOrD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhanLoai]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhanLoai](
	[Tenloai] [nvarchar](20) NOT NULL,
	[MaLoai] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaLoai] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SanPham]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SanPham](
	[MaSP] [int] NOT NULL,
	[TenSP] [nvarchar](max) NULL,
	[GiaSp] [int] NULL,
	[GiaGiam] [int] NULL,
	[SoLuong] [int] NOT NULL,
	[Hinh1] [nvarchar](max) NULL,
	[Hinh2] [nvarchar](max) NULL,
	[Hinh3] [nvarchar](max) NULL,
	[Hinh4] [nvarchar](max) NULL,
	[Hinh5] [nvarchar](max) NULL,
	[Mota] [nvarchar](max) NULL,
	[Thongso] [nvarchar](max) NULL,
	[MaLoai] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaSP] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[MaUser] [int] IDENTITY(1,1) NOT NULL,
	[TenUser] [nvarchar](20) NOT NULL,
	[sdt] [nvarchar](11) NOT NULL,
	[email] [varchar](100) NULL,
	[DiaChi] [nvarchar](100) NULL,
	[NgaySinh] [date] NULL,
	[TK] [varchar](20) NOT NULL,
	[Pass] [varchar](20) NOT NULL,
	[Roleuser] [nvarchar](10) NULL,
	[Hinh] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[MaUser] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vourcher]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vourcher](
	[MaKM] [int] NOT NULL,
	[Uudai] [int] NULL,
	[ThongTinUuDai] [nvarchar](max) NULL,
	[MaSP] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaKM] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YeuThich]    Script Date: 10/22/2023 9:37:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YeuThich](
	[MaYT] [int] NOT NULL,
	[MaSP] [int] NOT NULL,
	[MaUser] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MaYT] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ChiTietDH]  WITH CHECK ADD FOREIGN KEY([MaDH])
REFERENCES [dbo].[DonHang] ([MaDH])
GO
ALTER TABLE [dbo].[ChiTietDH]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[DonHang]  WITH CHECK ADD FOREIGN KEY([MaUser])
REFERENCES [dbo].[Users] ([MaUser])
GO
ALTER TABLE [dbo].[Mau_sp]  WITH CHECK ADD FOREIGN KEY([Mamau])
REFERENCES [dbo].[Mau] ([Mamau])
GO
ALTER TABLE [dbo].[Mau_sp]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[OrdersDetail]  WITH CHECK ADD FOREIGN KEY([MaOr])
REFERENCES [dbo].[Orders] ([MaOr])
GO
ALTER TABLE [dbo].[OrdersDetail]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[SanPham]  WITH CHECK ADD FOREIGN KEY([MaLoai])
REFERENCES [dbo].[PhanLoai] ([MaLoai])
GO
ALTER TABLE [dbo].[Vourcher]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[YeuThich]  WITH CHECK ADD FOREIGN KEY([MaSP])
REFERENCES [dbo].[SanPham] ([MaSP])
GO
ALTER TABLE [dbo].[YeuThich]  WITH CHECK ADD FOREIGN KEY([MaUser])
REFERENCES [dbo].[Users] ([MaUser])
GO
