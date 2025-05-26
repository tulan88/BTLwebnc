CREATE DATABASE BTLw
ON
(
    NAME = 'BTLw',
    FILENAME = 'D:\LapTrinhWebNC\BTLweb\Database.mdf',
    SIZE = 5MB,
    MAXSIZE = UNLIMITED,
    FILEGROWTH = 10%
)
GO

USE BTLw
GO

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
	FullName NVARCHAR(100),
    Birthday DATE,
    Email VARCHAR(255) UNIQUE NOT NULL,
    Password VARCHAR(255) NOT NULL,
    Role int NULL -- 0: User, 1: Admin
	State int NULL -- 0: không hợp lệ (chưa xác nhận), 1: hợp lệ (đã xác nhận (email))
)
GO

CREATE TABLE Categories (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName NVARCHAR(100) NOT NULL,
    Slug VARCHAR(100) UNIQUE NOT NULL
)
GO

CREATE TABLE Articles (
    ArticleID INT PRIMARY KEY IDENTITY(1,1),
    CategoryID INT NOT NULL,
    UserID INT NOT NULL,
    Title NVARCHAR(255) NOT NULL,
    Slug VARCHAR(255) UNIQUE NOT NULL,
    Content NTEXT,
    ImageURL VARCHAR(255),
    PostedTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
)

GO

CREATE TABLE Subscriptions (
    SubscriptionID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    CategoryID INT NOT NULL,
    SubscribedTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (CategoryID) REFERENCES Categories(CategoryID)
)
GO

CREATE TABLE Newsletters (
    NewsletterID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NOT NULL,
    ArticleID INT NOT NULL,
    SentTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (UserID) REFERENCES Users(UserID),
    FOREIGN KEY (ArticleID) REFERENCES Articles(ArticleID)
)
GO

CREATE TABLE Comments (
    CommentID INT PRIMARY KEY IDENTITY(1,1),
    ArticleID INT NOT NULL,
    UserID INT NOT NULL,
    Content NTEXT NOT NULL,
    CommentedTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ArticleID) REFERENCES Articles(ArticleID),
    FOREIGN KEY (UserID) REFERENCES Users(UserID)
)
GO

select * from Users

INSERT INTO Users (FullName, Birthday, Email, Password, Role, State)
VALUES 
(N'Nguyễn Văn A', '1998-01-15', 'nguyenvana@example.com', 'Pass123@', 0,1),
(N'Trần Thị B', '1995-06-20', 'tranthib@example.com', 'Pass123@', 0,1),
(N'Lê Văn C', '2000-12-05', 'levanc@example.com', 'Pass123@', 0, 1),
(N'Phạm Thị D', '1999-09-10', 'phamthid@example.com', 'Pass123@', 0, 1),
(N'Hoàng Văn E', '1997-03-22', 'hoangvane@example.com', 'Pass123@', 0, 1),
(N'Đặng Thị F', '1994-04-18', 'dangthif@example.com', 'Pass123@', 0,1),
(N'Vũ Văn G', '1996-07-30', 'vuvang@example.com', 'Pass123@', 0,1),
(N'Bùi Thị H', '1993-11-25', 'buithih@example.com', 'Pass123@', 0,1),
(N'Ngô Văn I', '1990-02-14', 'ngovani@example.com', 'Pass123@', 0,1),
(N'Mai Thị J', '1992-08-09', 'maithij@example.com', 'Pass123@', 0,1);
(N'Nguyễn Thị Tú Lan', '1992-08-09', 'tulan246135@gmail.com', '$2a$11$qSy/tjxNqoBibKMtwSq7QeBk0NM7vwjNBdY7LiyroW3aeWJquxqYG', 0,1);
GO


INSERT INTO Categories (CategoryName, Slug) VALUES
(N'Thế giới', 'the-gioi'),
(N'Kinh doanh', 'kinh-doanh'),
(N'Giáo dục', 'giao-duc'),
(N'Công nghệ', 'cong-nghe'),
(N'Thể thao', 'the-thao')
GO

select * from Articles
INSERT INTO Articles (CategoryID, UserID, Title, Slug, Content, ImageURL) VALUES
(1, 6, N'Tình hình thế giới hôm nay', 'tinh-hinh-the-gioi',
N'Trên toàn cầu, tình hình chính trị và kinh tế đang có nhiều biến động đáng chú ý. Chiến sự tại một số khu vực Trung Đông và Đông Âu tiếp tục leo thang, kéo theo làn sóng di cư và khủng hoảng nhân đạo nghiêm trọng. Tổ chức Liên Hợp Quốc đã kêu gọi các bên liên quan đối thoại hòa bình, đồng thời huy động nguồn lực hỗ trợ người dân ở vùng chiến sự. Ở châu Á, căng thẳng giữa một số quốc gia trong khu vực cũng khiến thị trường tài chính biến động. Bên cạnh đó, ảnh hưởng của biến đổi khí hậu đang ngày càng rõ rệt, với hàng loạt đợt nắng nóng cực đoan và thiên tai liên tiếp tại nhiều châu lục. Nhiều chính phủ đã ban hành các chính sách khẩn cấp để ứng phó và chuyển hướng sang phát triển năng lượng bền vững. Tình hình thế giới trong hôm nay không chỉ đặt ra thách thức lớn đối với các nhà lãnh đạo mà còn đòi hỏi sự đoàn kết toàn cầu nhằm vượt qua các khủng hoảng chung và hướng đến một tương lai ổn định, phát triển.', 
'image/img1.png'),

(2, 7, N'Giá cổ phiếu tăng mạnh', 'gia-co-phieu-tang',
N'Thị trường chứng khoán trong nước hôm nay đã có phiên giao dịch tích cực với sắc xanh bao phủ hầu hết các nhóm ngành. Đặc biệt, nhóm cổ phiếu công nghệ và bất động sản dẫn đầu đà tăng, thu hút lượng lớn nhà đầu tư trong và ngoài nước. VN-Index tăng hơn 20 điểm, vượt mốc 1.200 điểm sau nhiều phiên giao dịch giằng co. Khối ngoại tiếp tục mua ròng gần 1000 tỷ đồng, tập trung vào các mã có nền tảng tài chính vững mạnh và tiềm năng tăng trưởng dài hạn. Các chuyên gia tài chính nhận định sự phục hồi của thị trường có thể duy trì nếu lạm phát được kiểm soát tốt và các chính sách hỗ trợ doanh nghiệp phát huy hiệu quả. Ngoài ra, việc Ngân hàng Nhà nước giữ vững chính sách tiền tệ ổn định cũng góp phần tạo niềm tin cho nhà đầu tư. Tuy nhiên, giới phân tích cũng cảnh báo nhà đầu tư cần thận trọng với các mã đầu cơ ngắn hạn, tránh chạy theo sóng mà không có chiến lược dài hơi. Dự báo trong những phiên tới, thị trường sẽ tiếp tục tăng trưởng nhưng có thể phân hóa rõ rệt giữa các nhóm ngành.', 
'image/img2.png'),
(3, 8, N'Cách học hiệu quả', 'cach-hoc-hieu-qua',
N'Nhiều học sinh, sinh viên hiện nay vẫn loay hoay trong việc tìm ra phương pháp học hiệu quả nhất. Việc học không chỉ là ghi nhớ mà còn là quá trình tư duy, phản biện và ứng dụng kiến thức vào thực tế. Một trong những cách học được đánh giá cao là phương pháp Pomodoro – chia nhỏ thời gian học thành từng phiên 25 phút, nghỉ 5 phút giúp tăng khả năng tập trung. Ngoài ra, việc kết hợp học qua video, làm bài tập thực hành và thảo luận nhóm cũng mang lại hiệu quả rõ rệt. Đặc biệt, ghi chú thông minh bằng sơ đồ tư duy giúp bạn nắm bắt vấn đề nhanh hơn. Bên cạnh đó, cần tạo một môi trường học tập tích cực, tránh xa yếu tố gây xao nhãng như mạng xã hội. Quan trọng không kém là giữ sức khỏe thể chất và tinh thần tốt: ngủ đủ giấc, ăn uống hợp lý, và tập thể dục đều đặn. Tự tạo động lực bằng mục tiêu rõ ràng cũng là chìa khóa giúp duy trì thói quen học tập bền vững.', 
'image/img3.png'),

(4, 9, N'Công nghệ AI mới', 'cong-nghe-ai-moi',
N'Công nghệ Trí tuệ nhân tạo (AI) đang ngày càng khẳng định vai trò then chốt trong mọi lĩnh vực, từ y tế, giáo dục đến sản xuất và tài chính. Trong năm nay, các mô hình ngôn ngữ lớn (LLM) như GPT và Claude đã có bước tiến vượt bậc trong khả năng hiểu và tạo ngôn ngữ tự nhiên, giúp các doanh nghiệp tự động hóa nhiều quy trình phức tạp. AI không chỉ dừng lại ở chatbot hay trợ lý ảo, mà còn đang được tích hợp sâu vào hệ thống quản trị, chăm sóc khách hàng và phân tích dữ liệu. Một xu hướng nổi bật khác là AI đạo đức, hướng đến việc phát triển công nghệ minh bạch, công bằng và không phân biệt đối xử. Tuy nhiên, sự phát triển mạnh mẽ của AI cũng đặt ra lo ngại về việc mất việc làm và rủi ro về dữ liệu cá nhân. Các chuyên gia khuyến nghị cần xây dựng khung pháp lý rõ ràng để kiểm soát và điều tiết AI, đồng thời tập trung đào tạo kỹ năng số cho người lao động để thích ứng với kỷ nguyên mới.', 
'image/img4.png'),

(5, 10, N'Bóng đá Việt Nam', 'bong-da-viet-nam',
N'Bóng đá Việt Nam đang bước vào giai đoạn chuyển mình với sự đầu tư mạnh mẽ từ cả Liên đoàn và các CLB. Sau thành công tại SEA Games và AFF Cup những năm gần đây, đội tuyển quốc gia tiếp tục được kỳ vọng sẽ vươn xa tại đấu trường châu lục. Việc trẻ hóa đội hình và chú trọng đào tạo cầu thủ từ lứa U19, U23 cho thấy chiến lược lâu dài và bài bản. Các trung tâm bóng đá học viện như PVF, HAGL-JMG cũng đóng vai trò quan trọng trong việc phát hiện và bồi dưỡng tài năng trẻ. Bên cạnh đó, sự chuyên nghiệp trong vận hành giải V.League, cùng với sự xuất hiện của các HLV ngoại chất lượng cao, giúp nâng tầm bóng đá nội địa. Tuy nhiên, thách thức vẫn còn đó: chất lượng sân bãi, tài chính và văn hóa cổ động cần được cải thiện. Bóng đá không chỉ là thể thao, mà còn là niềm tự hào dân tộc. Cần sự chung tay từ cộng đồng, doanh nghiệp và chính quyền để đưa bóng đá Việt Nam vươn ra thế giới.', 
'image/img5.png'),

(1, 10, N'Tin thế giới mới nhất', 'tin-the-gioi-moi',
N'Trong 24 giờ qua, thế giới chứng kiến nhiều sự kiện nổi bật. Hội nghị thượng đỉnh G20 diễn ra tại Ấn Độ đã kết thúc với tuyên bố chung tập trung vào vấn đề khí hậu và phát triển bền vững. Trong khi đó, căng thẳng giữa Trung Quốc và Đài Loan tiếp tục leo thang sau khi một số tàu chiến được điều động đến vùng biển tranh chấp. Tại châu Âu, lạm phát tuy đã giảm nhẹ nhưng vẫn ở mức cao, khiến các ngân hàng trung ương duy trì chính sách thắt chặt tiền tệ. Ở châu Phi, xung đột sắc tộc tại Sudan khiến hàng chục nghìn người phải sơ tán. Mặt khác, thị trường chứng khoán toàn cầu phục hồi nhẹ sau khi Mỹ công bố chỉ số việc làm khả quan. Trong lĩnh vực công nghệ, nhiều công ty lớn công bố hợp tác trong lĩnh vực AI nhằm tạo ra giải pháp thông minh hơn cho y tế và giáo dục. Có thể thấy, thế giới đang trong thời kỳ biến động nhanh chóng, đòi hỏi sự hợp tác và đối thoại không ngừng để hướng đến một tương lai ổn định và bền vững.', 
'image/img12.png'),

(2, 9, N'Kinh doanh mùa dịch', 'kinh-doanh-mua-dich',
N'Đại dịch COVID-19 đã làm thay đổi hoàn toàn cách thức vận hành kinh doanh trên toàn thế giới. Nhiều doanh nghiệp nhỏ buộc phải đóng cửa, nhưng cũng có không ít đơn vị thích ứng nhanh chóng, chuyển đổi mô hình sang kinh doanh trực tuyến và giao hàng tận nơi. Những ngành hàng như thực phẩm sạch, y tế, công nghệ và dịch vụ giao hàng bùng nổ trong thời kỳ giãn cách. Sự xuất hiện của các nền tảng số như Shopee, Lazada, và Facebook Marketplace giúp nhiều doanh nghiệp duy trì và mở rộng hoạt động. Bên cạnh đó, việc sử dụng công nghệ thanh toán không tiếp xúc, hóa đơn điện tử và hệ thống quản trị từ xa trở thành xu hướng không thể đảo ngược. Doanh nghiệp cần linh hoạt, sáng tạo và tập trung vào trải nghiệm khách hàng để vượt qua thách thức và đón đầu cơ hội hậu đại dịch. Đây là thời điểm để đổi mới, tái cấu trúc và chuẩn bị cho một nền kinh tế kỹ thuật số toàn diện hơn trong tương lai gần.', 
'image/img22.png'),

(3, 8, N'Học online hiệu quả', 'hoc-online-hieu-qua',
N'Học online đã trở thành xu hướng tất yếu sau đại dịch, nhưng hiệu quả của hình thức này vẫn còn là vấn đề gây tranh cãi. Một số học sinh có thể học tập linh hoạt, chủ động và sáng tạo hơn nhờ tài nguyên số dồi dào. Tuy nhiên, việc thiếu môi trường tương tác trực tiếp khiến không ít người học mất tập trung, khó tiếp thu kiến thức. Để học online hiệu quả, trước hết cần có một không gian học tập yên tĩnh, thiết bị ổn định và kết nối mạng tốt. Ngoài ra, cần xây dựng lịch học rõ ràng, chia nhỏ nội dung theo thời lượng hợp lý. Việc ghi chú, đặt câu hỏi và chủ động trao đổi với giảng viên qua các công cụ như Zoom, Google Meet hay Moodle giúp tăng sự tương tác. Hơn nữa, sử dụng nền tảng hỗ trợ như Quizlet, Kahoot hay Canva cũng giúp việc học thú vị và dễ nhớ hơn. Quan trọng nhất là tính kỷ luật và khả năng tự học – hai yếu tố then chốt để học online đạt kết quả tốt.', 
'image/img32.png'),

(4, 7, N'Ứng dụng Blockchain', 'ung-dung-blockchain',
N'Blockchain không chỉ giới hạn trong tiền mã hóa như Bitcoin hay Ethereum, mà đã và đang được ứng dụng rộng rãi trong nhiều lĩnh vực khác nhau. Trong chuỗi cung ứng, công nghệ này giúp theo dõi hàng hóa từ nơi sản xuất đến tay người tiêu dùng một cách minh bạch và an toàn. Trong y tế, blockchain được sử dụng để lưu trữ và bảo mật hồ sơ bệnh án, đảm bảo dữ liệu không bị giả mạo. Ngành giáo dục cũng bắt đầu ứng dụng blockchain để xác minh văn bằng, chứng chỉ giúp chống gian lận học thuật. Ngoài ra, chính phủ nhiều nước đang thử nghiệm dùng blockchain trong bỏ phiếu điện tử và quản lý tài sản công. Điểm mạnh của blockchain nằm ở tính phi tập trung, bảo mật cao và không thể chỉnh sửa sau khi ghi nhận. Tuy nhiên, vẫn còn nhiều thách thức về khả năng mở rộng, chi phí và tiêu thụ năng lượng. Nếu vượt qua được những rào cản này, blockchain có tiềm năng cách mạng hóa cách chúng ta quản lý dữ liệu và tin tưởng vào hệ thống kỹ thuật số.', 
'image/img42.png'),

(5, 6, N'Vô địch AFF Cup', 'vo-dich-aff-cup',
N'Đội tuyển Việt Nam đã có một chiến dịch AFF Cup ấn tượng và đầy cảm xúc. Sau vòng bảng với thành tích bất bại, thầy trò HLV Park Hang-seo tiến vào bán kết với phong độ ổn định và tinh thần chiến đấu cao. Trận chung kết gặp Thái Lan là màn đối đầu được mong chờ nhất, thu hút hàng triệu người theo dõi cả trong nước và quốc tế. Với lối chơi kỷ luật, pressing mạnh và những pha dứt điểm chuẩn xác, đội tuyển đã giành chiến thắng với tỷ số 2-1, chính thức lên ngôi vô địch. Người hâm mộ cả nước vỡ òa trong niềm vui, đường phố rực rỡ cờ hoa ăn mừng chiến tích lịch sử. Thành công này không chỉ là phần thưởng xứng đáng cho nỗ lực không ngừng của toàn đội, mà còn khẳng định vị thế của bóng đá Việt Nam trong khu vực. Nhiều cầu thủ trẻ như Quang Hải, Văn Hậu, hay Tiến Linh trở thành thần tượng mới của giới trẻ. Chiến thắng AFF Cup là bước đệm vững chắc để đội tuyển hướng đến mục tiêu lớn hơn như vòng loại World Cup trong tương lai.', 
'image/img52.png');
GO

INSERT INTO Subscriptions (UserID, CategoryID) VALUES
(1, 1), (1, 2), (1, 3),
(2, 3),(2, 4),
(3, 1), (3, 4), (3, 5),
(4, 5),
(5, 2)
GO

--gửi bài của 
INSERT INTO Newsletters (UserID, ArticleID) VALUES
(6, 1), (6, 10), 
(7, 2), (7, 9),
(8, 3), (8,8),
(9, 4), (9,7),
(10, 5), (10,6)
GO

INSERT INTO Comments (ArticleID, UserID, Content) VALUES
(1, 6, N'Bài viết rất hay, thông tin cập nhật kịp thời.'),
(2, 7, N'Cảm ơn tác giả, mình đã hiểu hơn về thị trường chứng khoán.'),
(3, 8, N'Mình cũng đang áp dụng Pomodoro, hiệu quả thật đấy!'),
(4, 9, N'AI đúng là xu hướng tất yếu, cảm ơn bài viết hữu ích.'),
(5, 10, N'Tuyệt vời, hy vọng bóng đá Việt Nam sẽ tiến xa hơn nữa.'),
(6, 6, N'Cập nhật nhanh và chi tiết, rất đáng đọc.'),
(7, 7, N'Đồng ý, kinh doanh mùa dịch đúng là thử thách lớn.'),
(8, 8, N'Học online cũng cần phải có kỷ luật nữa.'),
(9, 9, N'Blockchain sẽ còn thay đổi nhiều ngành nữa trong tương lai.'),
(10, 10, N'Chiến thắng xứng đáng, quá tuyệt vời!')
GO

