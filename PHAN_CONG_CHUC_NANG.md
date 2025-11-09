# BÁO CÁO PHÂN TÍCH VÀ PHÂN CÔNG CHỨC NĂNG
## HỆ THỐNG QUẢN LÝ MẠNG XÃ HỘI

---

## MỤC LỤC

I. TỔNG QUAN DỰ ÁN  
II. PHÂN CHIA CÔNG VIỆC TRONG NHÓM  
III. CHI TIẾT CHỨC NĂNG ADMIN  
IV. CHI TIẾT CHỨC NĂNG USER  
V. KIẾN TRÚC HỆ THỐNG  
VI. KẾ HOẠCH THỰC HIỆN  
VII. KẾT LUẬN

---

## I. TỔNG QUAN DỰ ÁN

### 1.1. Thông tin dự án

**Tên hệ thống**: Social Media Management System  
**Công nghệ sử dụng**: C# WinForms .NET 8.0  
**Mục đích**: Xây dựng hệ thống quản lý mạng xã hội nội bộ  
**Kiến trúc**: Desktop Application  
**Cơ sở dữ liệu**: CSV Files  
**Ngôn ngữ lập trình**: C# 12.0

### 1.2. Phạm vi dự án

Hệ thống được thiết kế để hỗ trợ 2 vai trò chính:

1. **Admin (Quản trị viên)**
   - Quản lý toàn bộ hệ thống
   - Kiểm duyệt nội dung
   - Xử lý báo cáo vi phạm
   - Thống kê và phân tích dữ liệu

2. **User (Người dùng)**
   - Sử dụng các chức năng mạng xã hội cơ bản
   - Tạo và quản lý bài viết
   - Tương tác với người dùng khác
   - Quản lý hồ sơ cá nhân

### 1.3. Mục tiêu dự án

- Xây dựng hệ thống quản lý mạng xã hội hoàn chỉnh
- Phân chia công việc rõ ràng giữa các thành viên
- Đảm bảo chất lượng code và hiệu suất
- Hoàn thành đúng tiến độ

---

## II. PHÂN CHIA CÔNG VIỆC TRONG NHÓM

### 2.1. Thành viên 1 - Admin Developer

**Trách nhiệm chính**:
- Phát triển toàn bộ chức năng quản trị hệ thống
- Xây dựng các module quản lý (User, Post, Comment, Report)
- Phát triển dashboard và analytics
- Cấu hình hệ thống

**Files phụ trách**:
- frmAdmin.cs (1149 dòng code)
- ucDashboard.cs (1987 dòng)
- ucUserManagementNew.cs
- ucPostManagement.cs
- ucCommentManagement.cs
- ucReportManagement.cs
- ucAnalyticsInsights.cs
- ucSettings.cs (1141 dòng)

**Số lượng module**: 7 UserControls  
**Ước tính công việc**: 5000 dòng code  
**Độ phức tạp**: Rất cao

### 2.2. Thành viên 2 - User Developer

**Trách nhiệm chính**:
- Phát triển toàn bộ chức năng người dùng cuối
- Xây dựng giao diện newsfeed
- Phát triển chức năng tạo/chỉnh sửa bài viết
- Quản lý hồ sơ và tương tác

**Files phụ trách**:
- frmNewsfeed.cs (994 dòng code)
- frmPost.cs
- ucPostDetail.cs
- frmInfor.cs
- PostControl.cs

**Số lượng module**: 3-4 UserControls  
**Ước tính công việc**: 3000 dòng code  
**Độ phức tạp**: Cao

### 2.3. Bảng so sánh khối lượng công việc

| Tiêu chí | Admin Developer | User Developer |
|----------|-----------------|----------------|
| Số form chính | 1 (frmAdmin) | 1 (frmNewsfeed) |
| Số UserControl | 7 modules | 3-4 modules |
| Dòng code ước tính | Khoảng 5000 dòng | Khoảng 3000 dòng |
| Độ phức tạp | Rất cao | Cao |
| Database operations | Nhiều (CRUD all entities) | Trung bình |
| UI complexity | Cao (Charts, Tables) | Trung bình |
| Business logic | Phức tạp | Đơn giản hơn |

---

## III. CHI TIẾT CHỨC NĂNG ADMIN (Thành viên 1)

### 3.1. Module Dashboard (Bảng điều khiển)

**File**: frm/UserControls/ucDashboard.cs (1987 dòng)

**Mô tả**: Module cung cấp cái nhìn tổng quan về tình trạng hệ thống thông qua các chỉ số thống kê và biểu đồ trực quan.

#### 3.1.1. Thống kê tổng quan thời gian thực

- Tổng số người dùng (Total Users)
- Tổng số bài viết (Total Posts)
- Tổng số báo cáo (Total Reports)
- Số người dùng hoạt động (Active Users)
- Tính toán tăng trưởng theo phần trăm so với kỳ trước

#### 3.1.2. Biểu đồ trực quan

- Biểu đồ cột: Thống kê bài viết theo ngày
- Biểu đồ đường: Xu hướng tăng trưởng người dùng
- Biểu đồ tròn: Phân bố loại báo cáo (Post/Comment/User)

#### 3.1.3. Hệ thống cảnh báo

- Báo cáo chưa xử lý (Pending Reports)
- Người dùng bị khóa (Locked Users)
- Bài viết bị ẩn (Hidden Posts)
- Cập nhật tự động mỗi 30 giây

#### 3.1.4. Phím tắt nhanh

- Tạo bài viết mới
- Quản lý người dùng
- Xem báo cáo
- Cài đặt hệ thống

#### 3.1.5. Hoạt động gần đây

- Hiển thị 5 hoạt động mới nhất trong hệ thống
- Bao gồm: Đăng ký mới, báo cáo, bài viết mới

#### 3.1.6. Top hoạt động

- Top 5 người dùng hoạt động nhất
- Top 5 bài viết nhiều tương tác nhất

#### 3.1.7. Bộ lọc thời gian

- 24 giờ qua
- 7 ngày qua
- 30 ngày qua
- Tùy chỉnh khoảng thời gian

**Services sử dụng**:
- DashboardService: Logic tính toán thống kê
- UserService: Lấy dữ liệu người dùng
- PostService: Lấy dữ liệu bài viết
- ReportService: Lấy dữ liệu báo cáo
- CommentService: Lấy dữ liệu bình luận

**Kỹ thuật yêu cầu**:
- TableLayoutPanel cho responsive layout
- Custom drawing cho biểu đồ
- Auto-refresh với Timer
- Async/await cho performance
- LINQ queries phức tạp

---

### 3.2. Module Quản lý người dùng

**File**: frm/UserControls/ucUserManagementNew.cs

**Mô tả**: Module quản lý toàn bộ người dùng trong hệ thống, bao gồm tạo mới, chỉnh sửa, khóa/mở khóa tài khoản.

#### 3.2.1. Danh sách người dùng

Hiển thị toàn bộ users trong DataGridView với các cột:
- UserID
- Username
- FullName
- Email
- Phone
- Role (Admin/User)
- Status (Active/Locked)
- Report Count
- Created At

#### 3.2.2. Tìm kiếm và lọc

**Tìm kiếm theo**:
- Username
- Email
- FullName

**Lọc theo**:
- Role: Admin hoặc User
- Status: Active hoặc Locked
- Số lần bị báo cáo

#### 3.2.3. Thao tác CRUD

**Create (Tạo mới)**:
- Admin có thể tạo user mới
- Validate thông tin đầu vào
- Tự động generate UserID

**Read (Đọc)**:
- Xem chi tiết hồ sơ user
- Hiển thị đầy đủ thông tin

**Update (Cập nhật)**:
- Chỉnh sửa thông tin user
- Validate dữ liệu trước khi lưu

**Delete (Xóa)**:
- Xóa user khỏi hệ thống
- Confirm trước khi xóa

#### 3.2.4. Quản lý trạng thái

- **Unlock User**: Mở khóa tài khoản bị khóa
- **Lock User**: Khóa tài khoản khi vi phạm
- **Ban User**: Cấm vĩnh viễn

#### 3.2.5. Quản lý vai trò

- Thăng cấp User thành Admin
- Hạ cấp Admin thành User
- Phân quyền truy cập

#### 3.2.6. Thống kê user

- Số bài viết đã đăng
- Số lượt thích đã nhận
- Số lượt bình luận đã nhận
- Số lần bị báo cáo

**Services sử dụng**:
- UserService: Thao tác CRUD
- PostService: Đếm bài viết của user
- ReportService: Đếm báo cáo về user

**Model sử dụng** (models/users.cs):
```
- UserID (Guid): ID duy nhất của user
- UserName (string): Tên đăng nhập
- Password (string): Mật khẩu (đã mã hóa)
- FullName (string): Họ tên đầy đủ
- Bio (string): Tiểu sử
- AvatarUrl (string): Đường dẫn ảnh đại diện
- Email (string): Email liên hệ
- Phone (string): Số điện thoại
- Gender (string): Giới tính
- DOB (DateTime): Ngày sinh
- Address (string): Địa chỉ
- CreatedAt (DateTime): Ngày tạo tài khoản
- StatusId (int): 0=Active, 1=Locked
- Role (int): 0=User, 1=Admin
- ReportCount (int): Số lần bị báo cáo
```

**Kỹ thuật yêu cầu**:
- DataGridView advanced (custom columns, cell formatting)
- Form validation
- Role-based access control
- CSV CRUD operations
- Error handling

---

### 3.3. Module Quản lý bài viết

**File**: frm/UserControls/ucPostManagement.cs

**Mô tả**: Module kiểm duyệt và quản lý toàn bộ bài viết trong hệ thống.

#### 3.3.1. Danh sách bài viết

Hiển thị tất cả posts trong hệ thống với các cột:
- PostID
- Author (Tác giả)
- Content (Nội dung - preview)
- Media (Ảnh/Video)
- Visibility (Public/Friends/Private)
- Likes Count
- Comments Count
- Created At
- Status (Active/Hidden/Deleted)

#### 3.3.2. Tìm kiếm và lọc

**Tìm kiếm theo**:
- PostID
- Nội dung bài viết
- Tác giả

**Lọc theo**:
- Visibility: Public, Friends, Private
- Trạng thái: Active, Hidden, Deleted
- Ngày đăng: Date range picker

#### 3.3.3. Xem chi tiết bài viết

- Xem toàn bộ nội dung bài viết
- Xem media đính kèm (ảnh/video)
- Xem danh sách người đã like
- Xem danh sách comments

#### 3.3.4. Kiểm duyệt nội dung

- **Approve**: Duyệt bài viết hợp lệ
- **Hide**: Ẩn bài viết vi phạm
- **Delete**: Xóa bài viết vĩnh viễn
- **Pin**: Ghim bài viết quan trọng

#### 3.3.5. Quản lý tương tác

- Xem danh sách người đã like
- Xem và quản lý comments
- Xóa comments spam hoặc toxic

#### 3.3.6. Thống kê bài viết

- Bài viết nhiều tương tác nhất
- Bài viết bị báo cáo nhiều nhất
- Xu hướng nội dung (trending topics)

**Services sử dụng**:
- PostService: CreatePost, GetAllPosts, UpdatePost, DeletePost, SoftDelete
- CommentService: Quản lý comments
- LikeService: Quản lý likes

**Model sử dụng** (models/posts.cs):
```
- PostID (int): ID bài viết
- UserID (Guid): ID người đăng
- Content (string): Nội dung bài viết
- MediaUrl (string): Đường dẫn ảnh/video
- Visibility (string): Public/Friends/Private
- LikesCount (int): Số lượt thích
- CommentsCount (int): Số bình luận
- CreatedAt (DateTime): Thời gian tạo
- UpdatedAt (DateTime): Thời gian cập nhật
- IsDeleted (bool): Đánh dấu đã xóa
```

**Kỹ thuật yêu cầu**:
- Soft delete pattern (IsDeleted flag)
- Image/Media preview trong DataGridView
- Rich text display
- Pagination cho performance
- Async loading

---

### 3.4. Module Quản lý bình luận

**File**: frm/UserControls/ucCommentManagement.cs

**Mô tả**: Module kiểm duyệt và quản lý bình luận trong hệ thống.

#### 3.4.1. Danh sách bình luận

Hiển thị tất cả comments với các cột:
- CommentID
- PostID (Bài viết được comment)
- Author (Người comment)
- Content (Nội dung)
- Likes Count
- Created At
- Status

#### 3.4.2. Tìm kiếm và lọc

**Tìm kiếm theo**:
- CommentID
- Nội dung comment
- Tác giả

**Lọc theo**:
- PostID: Xem comments của một bài viết cụ thể
- Trạng thái: Active, Hidden, Deleted

#### 3.4.3. Kiểm duyệt comment

- **Approve**: Duyệt comment hợp lệ
- **Hide**: Ẩn comment spam/toxic
- **Delete**: Xóa comment vi phạm

#### 3.4.4. Phát hiện spam

- Comments có từ ngữ vi phạm
- Comments spam lặp đi lặp lại
- Comments từ users bị báo cáo nhiều lần

**Services sử dụng**:
- CommentService: CRUD operations

**Model sử dụng** (models/comments.cs):
```
- CommentID (int): ID comment
- PostID (int): ID bài viết
- UserID (Guid): ID người comment
- Content (string): Nội dung
- LikesCount (int): Số lượt thích
- CreatedAt (DateTime): Thời gian tạo
- IsDeleted (bool): Đánh dấu đã xóa
```

---

### 3.5. Module Quản lý báo cáo

**File**: frm/UserControls/ucReportManagement.cs

**Mô tả**: Module xử lý các báo cáo vi phạm từ người dùng.

#### 3.5.1. Danh sách báo cáo

Hiển thị tất cả reports với các cột:
- ReportID
- Type (Post/Comment/User)
- ContentID
- Reporter (Người báo cáo)
- Reported User (Người bị báo cáo)
- Reason (Lý do)
- Status (Pending/Approved/Rejected)
- Created At

#### 3.5.2. Phân loại báo cáo

- **Post Reports**: Báo cáo bài viết vi phạm
- **Comment Reports**: Báo cáo bình luận vi phạm
- **User Reports**: Báo cáo người dùng vi phạm

#### 3.5.3. Xử lý báo cáo

- **Review**: Xem chi tiết nội dung bị báo cáo
- **Approve**: Chấp nhận báo cáo và thực hiện xử phạt
- **Reject**: Từ chối báo cáo (không vi phạm)
- **Add Note**: Thêm ghi chú của admin

#### 3.5.4. Hành động khi approve

- **Warning**: Cảnh cáo user
- **Hide Content**: Ẩn nội dung vi phạm
- **Lock User**: Khóa tài khoản tạm thời
- **Ban User**: Cấm vĩnh viễn

#### 3.5.5. Thống kê báo cáo

- Số báo cáo pending/resolved
- Top users bị báo cáo nhiều nhất
- Loại vi phạm phổ biến nhất

**Services sử dụng**:
- ReportService: CreateReport, GetAllReports, GetPendingReports, ApproveReport, RejectReport, AddReviewNote

**Model sử dụng** (models/report.cs):
```
- ReportID (int): ID báo cáo
- ReportType (string): Post/Comment/User
- ContentID (int): ID nội dung bị báo cáo
- ReportedAt (DateTime): Thời gian báo cáo
- ReporterUserID (Guid): ID người báo cáo
- ReportedUserID (Guid): ID người bị báo cáo
- Reason (string): Lý do báo cáo
- Status (string): Pending/Approved/Rejected
- ReviewerID (Guid): ID admin xử lý
- ReviewedAt (DateTime): Thời gian xử lý
- AdminNote (string): Ghi chú admin
- Action (string): Warning/Hide/Lock/Ban
```

**Workflow xử lý báo cáo**:
```
[User Report] → [Pending] → [Admin Review] → [Approve/Reject]
                                 ↓
                          [Take Action] → [Resolved]
```

---

### 3.6. Module Thống kê và phân tích

**File**: frm/UserControls/ucAnalyticsInsights.cs

**Mô tả**: Module cung cấp các báo cáo và phân tích chi tiết về hoạt động hệ thống.

#### 3.6.1. Analytics tổng quan

- Tăng trưởng người dùng theo thời gian
- Lượng bài viết theo ngày/tuần/tháng
- Engagement rate (tỷ lệ tương tác)
- User retention (tỷ lệ giữ chân người dùng)

#### 3.6.2. Insights về nội dung

- Trending topics (chủ đề đang hot)
- Peak hours (giờ cao điểm hoạt động)
- Content types phổ biến nhất
- Viral posts (bài viết lan truyền nhanh)

#### 3.6.3. User behavior

- Active users vs Inactive users
- User demographics (giới tính, độ tuổi)
- Geographic distribution (nếu có dữ liệu địa lý)
- User activity patterns

#### 3.6.4. Reports và Exports

- Export báo cáo dạng PDF
- Export báo cáo dạng Excel
- Schedule reports (báo cáo định kỳ)
- Email reports tự động

**Services sử dụng**:
- DashboardService: Tính toán metrics và insights

---

### 3.7. Module Cài đặt hệ thống

**File**: frm/UserControls/ucSettings.cs (1141 dòng)

**Mô tả**: Module cấu hình toàn bộ hệ thống.

#### 3.7.1. Cài đặt chung

- **System Name**: Tên hiển thị của hệ thống
- **Theme**: Light/Dark/Auto
- **Session Timeout**: Thời gian timeout session (phút)
- **Maintenance Mode**: Bật/tắt chế độ bảo trì

#### 3.7.2. Email Configuration

- **SMTP Server**: Cấu hình máy chủ email
- **SMTP Port**: Cổng kết nối
- **Email/Password**: Thông tin đăng nhập
- **Test Email**: Chức năng test gửi email
- **Email Templates**: Mẫu email cho các mục đích khác nhau

**Chức năng email**:
- Gửi email xác thực đăng ký
- Gửi email reset password
- Gửi email thông báo hệ thống

#### 3.7.3. Backup và Restore

- **Backup Now**: Sao lưu toàn bộ dữ liệu ngay lập tức
- **Auto Backup**: Tự động sao lưu theo lịch
- **Backup Location**: Chọn thư mục lưu backup
- **Backup History**: Lịch sử các lần backup
- **Restore**: Khôi phục dữ liệu từ backup

**Định dạng backup**:
- ZIP file chứa tất cả CSV files
- Metadata: Timestamp, version, file count
- Checksum validation

#### 3.7.4. Data Management

- **Export Data**: Xuất dữ liệu ra CSV/JSON
- **Import Data**: Nhập dữ liệu từ file external
- **Clear Cache**: Xóa cache hệ thống
- **Clean Old Data**: Xóa dữ liệu cũ không dùng

#### 3.7.5. Security Settings

- **Password Policy**: Quy định về mật khẩu
  - Độ dài tối thiểu
  - Yêu cầu ký tự đặc biệt
  - Yêu cầu chữ hoa/thường/số
- **Ban Rules**: Quy tắc tự động ban user
  - Số lần báo cáo → tự động ban
  - Thời gian ban tự động
- **Two-factor Authentication**: Xác thực 2 yếu tố (nếu có)

#### 3.7.6. Logs và Monitoring

- **System Logs**: Log hoạt động hệ thống
- **User Activity Logs**: Log hoạt động người dùng
- **Error Logs**: Log các lỗi xảy ra
- **Log Retention**: Thời gian lưu trữ logs

**Services sử dụng**:
- GlobalSettings: Quản lý cài đặt toàn cục

**Properties của GlobalSettings**:
```
- SystemName (string): Tên hệ thống
- Theme (string): Light/Dark/Auto
- SessionTimeout (int): Timeout (phút)
- MaintenanceMode (bool): Chế độ bảo trì
- SmtpServer, SmtpPort, SmtpEmail, SmtpPassword
- BackupPath, AutoBackupEnabled, BackupInterval
```

**Methods quan trọng**:
- ApplyTheme(): Áp dụng theme cho toàn hệ thống
- UpdateSystemName(): Cập nhật tên hệ thống
- SetAllSettings(): Lưu tất cả cài đặt

**Chức năng đã hoàn thành**:
- Real SMTP email testing với HTML templates
- Real ZIP backup/restore với file history
- Enhanced Export/Import với metadata
- Global theme system (Light/Dark)
- Immediate theme application
- System name update across all forms

---

## IV. CHI TIẾT CHỨC NĂNG USER (Thành viên 2)

### 4.1. Module Newsfeed (Bảng tin)

**File**: frm/frmNewsfeed.cs (994 dòng)

**Mô tả**: Module hiển thị bảng tin với danh sách các bài viết của người dùng và bạn bè.

#### 4.1.1. Hiển thị bài viết

- Hiển thị danh sách bài viết theo thời gian đăng
- Lazy loading: Tải dần khi scroll
- Pull to refresh: Kéo xuống để refresh
- Real-time updates: Cập nhật tự động

#### 4.1.2. Tạo bài viết

- **Text Post**: Viết status text
- **Photo Post**: Đăng ảnh
- **Video Post**: Đăng video
- **Schedule Post**: Hẹn giờ đăng bài
- **Visibility**: Chọn độ hiển thị (Public/Friends/Private)

#### 4.1.3. Tương tác với bài viết

- **Like**: Thích bài viết
- **Comment**: Bình luận
- **Share**: Chia sẻ bài viết (nếu có)
- **Save**: Lưu bài viết để đọc sau

#### 4.1.4. Bộ lọc và tìm kiếm

**Tìm kiếm**:
- Tìm kiếm bài viết theo nội dung
- Tìm kiếm người dùng

**Bộ lọc**:
- Filter by Type: Text/Photo/Video
- Filter by Date: Theo khoảng thời gian
- Filter by PostID: Tìm bài viết cụ thể

#### 4.1.5. Cấu trúc giao diện

**Header**:
- Logo hệ thống
- Search box
- User avatar

**Sidebar**:
- Newsfeed
- Friends
- Messages
- Groups
- Events
- Saved Posts

**Main Content**:
- Create post box
- List of posts (FlowLayoutPanel)
- Load more button

**Components sử dụng**:
- FlowLayoutPanel: Container cho danh sách posts
- PostControl.cs: UserControl hiển thị một post
- Filter section: ComboBox, DateTimePicker

**Services sử dụng**:
- PostService: CRUD bài viết
- UserService: Lấy thông tin user
- AuthSessionService: Current user session

---

### 4.2. Module Hồ sơ cá nhân

**File**: frm/frmInfor.cs

**Mô tả**: Module quản lý hồ sơ cá nhân của người dùng.

#### 4.2.1. Xem hồ sơ

**Thông tin hiển thị**:
- Avatar: Ảnh đại diện
- Cover Photo: Ảnh bìa
- Thông tin cơ bản: FullName, Bio, Email, Phone
- Thông tin mở rộng: Gender, DOB, Address
- Số liệu thống kê:
  - Posts count
  - Friends count
  - Followers count

#### 4.2.2. Chỉnh sửa hồ sơ

- **Update Info**: Cập nhật thông tin cơ bản
- **Change Avatar**: Đổi ảnh đại diện
- **Change Cover**: Đổi ảnh bìa
- **Update Bio**: Cập nhật tiểu sử cá nhân

#### 4.2.3. Bài viết của tôi

- Danh sách bài viết đã đăng
- Bài viết đã lưu (saved posts)
- Bài viết đã thích (liked posts)

#### 4.2.4. Privacy Settings

- Ai có thể xem hồ sơ
- Ai có thể bình luận bài viết của tôi
- Ai có thể gửi tin nhắn cho tôi

**Services sử dụng**:
- UserService: Update profile
- PostService: Get my posts

---

### 4.3. Module Chi tiết bài viết

**File**: frm/UserControls/ucPostDetail.cs

**Mô tả**: Module hiển thị chi tiết một bài viết cụ thể.

#### 4.3.1. Xem chi tiết bài viết

- Nội dung đầy đủ của bài viết
- Media (ảnh/video) ở full size
- Thông tin người đăng
- Thời gian đăng bài
- Số lượt like và comment

#### 4.3.2. Comments Section

- Danh sách tất cả comments
- Viết comment mới
- Reply to comment (trả lời comment)
- Like comment
- Delete own comment (xóa comment của mình)

#### 4.3.3. Likes Section

- Danh sách người đã like
- Thống kê reactions (nếu có nhiều loại)

#### 4.3.4. Actions

- **Edit**: Chỉnh sửa (nếu là bài của mình)
- **Delete**: Xóa bài (nếu là bài của mình)
- **Report**: Báo cáo bài viết vi phạm
- **Hide**: Ẩn bài viết (không muốn thấy)

**Services sử dụng**:
- PostService: Get post detail
- CommentService: CRUD comments
- LikeService: Like/Unlike
- ReportService: Report post

---

### 4.4. Module Tạo và chỉnh sửa bài viết

**File**: frm/frmPost.cs

**Mô tả**: Module tạo mới và chỉnh sửa bài viết.

#### 4.4.1. Soạn bài viết

- **Text Editor**: Viết nội dung (hỗ trợ multiline)
- **Media Upload**: Chọn và upload ảnh/video
- **Emoji Picker**: Chọn emoji
- **Hashtags**: Thêm hashtags

#### 4.4.2. Privacy và Visibility

- **Public**: Công khai cho tất cả mọi người
- **Friends**: Chỉ bạn bè mới xem được
- **Private**: Chỉ mình tôi xem được

#### 4.4.3. Advanced Options

- **Schedule**: Hẹn giờ đăng bài
- **Location**: Thêm vị trí check-in
- **Tag Friends**: Tag bạn bè vào bài viết

#### 4.4.4. Preview và Post

- **Preview**: Xem trước bài viết
- **Post**: Đăng bài ngay
- **Save Draft**: Lưu nháp để đăng sau

**Services sử dụng**:
- PostService: CreatePost(), UpdatePost()

**Validation rules**:
- Content không được rỗng
- Media file size < 5MB
- Supported formats: jpg, png, gif, mp4
- Content length < 10000 characters

---

### 4.5. Module Bạn bè (Friends)

**Mô tả**: Module quản lý danh sách bạn bè và lời mời kết bạn.

#### 4.5.1. Danh sách bạn bè

- Hiển thị tất cả bạn bè
- Tìm kiếm bạn bè theo tên
- Lọc theo: Online/Offline, Gần đây

#### 4.5.2. Lời mời kết bạn

- Gửi lời mời kết bạn
- Nhận lời mời kết bạn
- Chấp nhận lời mời
- Từ chối lời mời

#### 4.5.3. Gợi ý kết bạn

- Dựa trên mutual friends (bạn chung)
- Dựa trên interests (sở thích)
- Dựa trên location (vị trí)

#### 4.5.4. Quản lý quan hệ

- Unfriend: Hủy kết bạn
- Block User: Chặn người dùng
- Unblock: Bỏ chặn

---

### 4.6. Module Tìm kiếm (Search)

**Mô tả**: Module tìm kiếm toàn diện trong hệ thống.

#### 4.6.1. Tìm kiếm toàn diện

**Loại tìm kiếm**:
- Users: Tìm người dùng
- Posts: Tìm bài viết
- Hashtags: Tìm theo hashtag

#### 4.6.2. Bộ lọc tìm kiếm

- Lọc theo thời gian
- Lọc theo loại nội dung
- Lọc theo người đăng

#### 4.6.3. Search History

- Lịch sử tìm kiếm của user
- Trending searches: Tìm kiếm phổ biến
- Clear search history

---

### 4.7. Module Thông báo (Notifications)

**Mô tả**: Module quản lý thông báo cho người dùng.

#### 4.7.1. Loại thông báo

- Like notification: Ai đó thích bài viết của bạn
- Comment notification: Ai đó bình luận bài viết của bạn
- Friend request: Lời mời kết bạn
- Mention notification: Ai đó tag bạn

#### 4.7.2. Quản lý thông báo

- Đánh dấu đã đọc
- Xóa thông báo
- Xóa tất cả thông báo

#### 4.7.3. Settings thông báo

- Bật/tắt từng loại thông báo
- Email notifications
- Push notifications (nếu có)

---

## V. KIẾN TRÚC HỆ THỐNG

### 5.1. Services Layer (Lớp dịch vụ)

Cả 2 thành viên sử dụng chung các Services sau:

#### 5.1.1. AuthSessionService

**Chức năng**:
- Quản lý session người dùng hiện tại
- CurrentUser: Property lưu user đang đăng nhập
- Login(): Đăng nhập
- Logout(): Đăng xuất
- IsAuthenticated(): Kiểm tra đã đăng nhập chưa

#### 5.1.2. UserService

**Methods**:
- GetUserById(Guid id): Lấy user theo ID
- GetAllUsers(): Lấy tất cả users
- CreateUser(User user): Tạo user mới
- UpdateUser(User user): Cập nhật user
- DeleteUser(Guid id): Xóa user
- LockUser(Guid id): Khóa user
- UnlockUser(Guid id): Mở khóa user

**File**: services/user.service.cs (518 dòng)

#### 5.1.3. PostService

**Methods**:
- GetAllPosts(): Lấy tất cả posts
- GetPostById(int id): Lấy post theo ID
- GetPostsByUser(Guid userId): Lấy posts của một user
- CreatePost(content, mediaUrl, visibility): Tạo post mới
- UpdatePost(Post post): Cập nhật post
- DeletePost(int id): Xóa post (soft delete)
- HidePost(int id): Ẩn post

**File**: services/post.service.cs (501 dòng)

#### 5.1.4. CommentService

**Methods**:
- GetCommentsByPost(int postId): Lấy comments của post
- CreateComment(postId, content): Tạo comment mới
- UpdateComment(Comment comment): Sửa comment
- DeleteComment(int id): Xóa comment

**File**: services/comment.service.cs

#### 5.1.5. LikeService

**Methods**:
- LikePost(int postId): Like bài viết
- UnlikePost(int postId): Bỏ like
- GetLikesByPost(int postId): Lấy danh sách likes

#### 5.1.6. ReportService

**Methods**:
- CreateReport(type, contentId, reason): Tạo báo cáo
- GetAllReports(): Lấy tất cả báo cáo (Admin only)
- GetPendingReports(): Lấy báo cáo chờ xử lý
- ApproveReport(int id, action): Duyệt báo cáo
- RejectReport(int id): Từ chối báo cáo

**File**: services/report.service.cs (271 dòng)

#### 5.1.7. DashboardService

**Methods**:
- GetTotalUsers(): Tổng số users
- GetTotalPosts(): Tổng số posts
- GetActiveUsers(timeRange): Users hoạt động
- GetGrowthRate(metric, timeRange): Tỷ lệ tăng trưởng

**File**: services/dashboard.service.cs

### 5.2. Models Layer (Lớp dữ liệu)

#### 5.2.1. User Model

**File**: models/users.cs

**Properties**:
```
- UserID (Guid): ID duy nhất
- UserName (string): Tên đăng nhập
- Password (string): Mật khẩu
- FullName (string): Họ tên
- Bio (string): Tiểu sử
- AvatarUrl (string): Ảnh đại diện
- Email (string): Email
- Phone (string): Số điện thoại
- Gender (string): Giới tính
- DOB (DateTime): Ngày sinh
- Address (string): Địa chỉ
- CreatedAt (DateTime): Ngày tạo
- StatusId (int): 0=Active, 1=Locked
- Role (int): 0=User, 1=Admin
- ReportCount (int): Số lần bị báo cáo
```

#### 5.2.2. Post Model

**File**: models/posts.cs

**Properties**:
```
- PostID (int): ID bài viết
- UserID (Guid): ID người đăng
- Content (string): Nội dung
- MediaUrl (string): Đường dẫn media
- Visibility (string): Public/Friends/Private
- LikesCount (int): Số lượt thích
- CommentsCount (int): Số bình luận
- CreatedAt (DateTime): Thời gian tạo
- UpdatedAt (DateTime): Thời gian cập nhật
- IsDeleted (bool): Đánh dấu xóa
```

#### 5.2.3. Comment Model

**File**: models/comments.cs

**Properties**:
```
- CommentID (int): ID comment
- PostID (int): ID bài viết
- UserID (Guid): ID người comment
- Content (string): Nội dung
- LikesCount (int): Số lượt thích
- CreatedAt (DateTime): Thời gian tạo
- IsDeleted (bool): Đánh dấu xóa
```

#### 5.2.4. Report Model

**File**: models/report.cs

**Properties**:
```
- ReportID (int): ID báo cáo
- ReportType (string): Post/Comment/User
- ContentID (int): ID nội dung bị báo cáo
- ReportedAt (DateTime): Thời gian báo cáo
- ReporterUserID (Guid): ID người báo cáo
- ReportedUserID (Guid): ID người bị báo cáo
- Reason (string): Lý do
- Status (string): Pending/Approved/Rejected
- ReviewerID (Guid): ID admin xử lý
- ReviewedAt (DateTime): Thời gian xử lý
- AdminNote (string): Ghi chú admin
- Action (string): Warning/Hide/Lock/Ban
```

#### 5.2.5. Like Model

**File**: models/likes.cs

**Properties**:
```
- LikeID (int): ID like
- PostID (int): ID bài viết
- UserID (Guid): ID người like
- CreatedAt (DateTime): Thời gian like
```

### 5.3. Utilities Layer

#### 5.3.1. GlobalSettings

**File**: utils/GlobalSettings.cs

**Purpose**: Quản lý cài đặt toàn cục của hệ thống

**Properties**:
- SystemName: Tên hệ thống
- Theme: Light/Dark/Auto
- SessionTimeout: Timeout session
- MaintenanceMode: Chế độ bảo trì

**Methods**:
- ApplyTheme(): Áp dụng theme
- UpdateSystemName(): Cập nhật tên hệ thống
- SetAllSettings(): Lưu tất cả settings

**Events**:
- SettingsChanged: Event khi settings thay đổi

#### 5.3.2. UIHelper

**File**: utils/UIHelper.cs

**Methods**:
- ApplyRoundedCorners(): Bo tròn góc control
- MakeCircular(): Tạo control hình tròn
- ShowNotification(): Hiển thị thông báo
- ValidateInput(): Validate input

#### 5.3.3. DashboardTheme

**File**: utils/DashboardTheme.cs

**Purpose**: Quản lý màu sắc và theme của dashboard

**Properties**:
- PrimaryColor
- SecondaryColor
- BackgroundLight
- BackgroundDark
- TextColor

---

## VI. KẾ HOẠCH THỰC HIỆN

### 6.1. Phase 1: Setup và Planning (Ngày 1-2)

**Cả 2 thành viên**:
1. Đọc và hiểu tài liệu phân công
2. Setup môi trường phát triển:
   - Visual Studio 2022
   - .NET 8.0 SDK
   - Git
3. Clone project về máy
4. Test build project
5. Phân chia Git branches

**Thỏa thuận**:
- Branch naming convention:
  - feature/admin-module-name
  - feature/user-module-name
- Commit message format:
  - [Admin] Add user management module
  - [User] Implement newsfeed UI
- Code review process
- Daily standup time

### 6.2. Phase 2: Development (Ngày 3-12)

**Admin Developer** - Sprint planning:

**Sprint 1 (Ngày 3-5)**: Dashboard và User Management
- Hoàn thiện ucDashboard.cs
  - Thống kê tổng quan
  - Biểu đồ
  - Alerts panel
  - Auto-refresh
- Hoàn thiện ucUserManagementNew.cs
  - DataGridView
  - CRUD operations
  - Lock/Unlock users

**Sprint 2 (Ngày 6-8)**: Content Moderation
- Hoàn thiện ucPostManagement.cs
  - DataGridView posts
  - View details
  - Hide/Delete posts
- Hoàn thiện ucCommentManagement.cs
  - DataGridView comments
  - Moderation features

**Sprint 3 (Ngày 9-11)**: Reports và Settings
- Hoàn thiện ucReportManagement.cs
  - Review workflow
  - Approve/Reject
- Hoàn thiện ucSettings.cs
  - Verify existing features
  - Add missing features

**Sprint 4 (Ngày 12)**: Analytics
- Hoàn thiện ucAnalyticsInsights.cs
  - Advanced charts
  - Insights

---

**User Developer** - Sprint planning:

**Sprint 1 (Ngày 3-5)**: Newsfeed Core
- Hoàn thiện frmNewsfeed.cs
  - Post list display
  - Search functionality
  - Filter features
- Hoàn thiện PostControl.cs
  - Display post
  - Like/Comment buttons

**Sprint 2 (Ngày 6-8)**: Post Creation
- Hoàn thiện frmPost.cs
  - Text editor
  - Media upload
  - Visibility selector
  - Schedule post

**Sprint 3 (Ngày 9-11)**: Post Interactions
- Hoàn thiện ucPostDetail.cs
  - Full post view
  - Comments section
  - Likes list
- Report functionality

**Sprint 4 (Ngày 12)**: Profile
- Hoàn thiện frmInfor.cs
  - View profile
  - Edit profile
  - Privacy settings

---

**Daily Workflow**:

**Daily Standup** (10 phút mỗi sáng):
- Đã làm gì hôm qua?
- Sẽ làm gì hôm nay?
- Có vấn đề gì cần support?

**Code Review**:
- Mỗi feature hoàn thành tạo Pull Request
- Người còn lại review code
- Approve thì merge vào main

**Testing**:
- Test local trước khi commit
- Test integration sau khi merge

### 6.3. Phase 3: Integration và Testing (Ngày 13-14)

**Ngày 13: Integration**
- Merge cả 2 phần lại
- Resolve conflicts
- Test tích hợp:
  - Admin tạo user, User login được không
  - Admin hide post, User không thấy post
  - User report, Admin nhận được report

**Ngày 14: Testing**
- Test các scenarios chính:
  - User registration flow
  - Post creation và moderation
  - Report workflow
  - Settings và theme
- Fix bugs phát hiện
- Optimize performance
- Code cleanup

### 6.4. Phase 4: Finalization (Ngày 15)

**Morning: Polish**
- UI/UX improvements
- Final code cleanup
- Documentation update

**Afternoon: Presentation Prep**
- Chuẩn bị demo scenarios
- Tạo slides giới thiệu
- Chuẩn bị Q&A
- Rehearsal demo

---

## VII. DELIVERABLES (Sản phẩm giao nộp)

### 7.1. Admin Developer giao nộp

**Code**:
1. frmAdmin.cs - Hoàn chỉnh với 7 modules
2. All admin UserControls:
   - ucDashboard.cs
   - ucUserManagementNew.cs
   - ucPostManagement.cs
   - ucCommentManagement.cs
   - ucReportManagement.cs
   - ucAnalyticsInsights.cs
   - ucSettings.cs
3. Admin services implementation (nếu cần thêm)
4. Unit tests (nếu có thời gian)

**Documentation**:
5. Admin User Guide
6. Technical documentation cho admin modules

### 7.2. User Developer giao nộp

**Code**:
1. frmNewsfeed.cs - Hoàn chỉnh
2. frmPost.cs - Create/Edit posts
3. ucPostDetail.cs - View details
4. frmInfor.cs - User profile
5. PostControl.cs - Post display component
6. User services implementation (nếu cần thêm)

**Documentation**:
7. User User Guide
8. Technical documentation cho user modules

### 7.3. Chung (Cả 2 người)

**Code**:
1. Merged codebase hoàn chỉnh
2. Test scenarios và results
3. Bug fixes list

**Documentation**:
4. Complete User Manual (Admin + User)
5. Technical Documentation
6. API Documentation (Services layer)

**Presentation**:
7. Demo video (5-10 phút)
8. Presentation slides (15-20 slides)
9. Q&A preparation document

---

## VIII. TIPS VÀ BEST PRACTICES

### 8.1. Cho Admin Developer

**Ưu tiên**:
1. Dashboard là trang chủ, làm đầu tiên
2. User Management là core, cần hoàn thiện tốt
3. Report Management cần workflow rõ ràng

**Kỹ thuật**:
- Sử dụng DataGridView hiệu quả
- Validation kỹ lưỡng (admin actions ảnh hưởng toàn hệ thống)
- Log mọi thao tác quan trọng
- Test với nhiều data (100+ users, 1000+ posts)
- Xử lý exception cẩn thận

**Performance**:
- Sử dụng async/await cho operations nặng
- Pagination cho danh sách lớn
- Cache data khi có thể
- Optimize LINQ queries

### 8.2. Cho User Developer

**Ưu tiên**:
1. Newsfeed là màn hình chính, cần đẹp và mượt
2. UI/UX quan trọng (user trải nghiệm trực tiếp)
3. Performance matters (lazy loading, optimization)

**Kỹ thuật**:
- Validate input ngăn spam và malicious content
- Responsive design support multiple screen sizes
- Error handling tốt với user-friendly messages
- Image optimization trước khi lưu

**UX Design**:
- Feedback ngay lập tức khi user thao tác
- Loading indicators cho async operations
- Confirmation dialogs cho actions quan trọng
- Keyboard shortcuts cho power users

### 8.3. Best Practices chung

**Communication**:
- Communicate thường xuyên, đừng làm im lặng
- Hỏi ngay khi stuck quá 30 phút
- Share knowledge và giúp đỡ nhau
- Daily standup nghiêm túc

**Code Quality**:
- Commit nhỏ, commit thường xuyên
- Test trước khi commit
- Write meaningful commit messages
- Code review kỹ càng
- Follow coding conventions

**Documentation**:
- Document while coding, không để cuối
- Add XML comments cho public methods
- Update README khi cần
- Keep documentation in sync với code

**Version Control**:
- Always pull before start working
- Resolve conflicts carefully
- Don't commit to main directly
- Use feature branches

---

## IX. SUPPORT RESOURCES

### 9.1. Khi gặp vấn đề

**Bước 1**: Đọc lại code hiện có
- Nhiều patterns đã có sẵn trong project
- Reference existing implementations

**Bước 2**: Debug với breakpoints
- Hiểu flow chạy của code
- Inspect variables và states

**Bước 3**: Google/StackOverflow
- Search với error messages
- Check Microsoft docs

**Bước 4**: Ask teammate
- Người kia có thể đã gặp vấn đề tương tự
- Pair programming khi cần

**Bước 5**: Ask instructor
- Last resort khi thực sự stuck

### 9.2. Tài liệu tham khảo

**Microsoft Docs**:
- C# WinForms Documentation
- LINQ Queries
- Async Programming
- File I/O

**Third-party Resources**:
- CodeProject articles
- StackOverflow
- GitHub repositories
- YouTube tutorials

**Project-specific**:
- BUILD_FIXES_SUMMARY.md
- DASHBOARD_IMPROVEMENTS.md
- ANALYTICS_INSIGHTS_README.md
- DASHBOARD_UI_SUMMARY.md

---

## X. KẾT LUẬN

### 10.1. Tóm tắt phân công

**Admin Developer (Thành viên 1)**:
- Chịu trách nhiệm: Backend logic, quản trị hệ thống
- Workload: Rất cao (5000 dòng code)
- Focus: Business logic, data management, security
- Modules: 7 UserControls quan trọng

**User Developer (Thành viên 2)**:
- Chịu trách nhiệm: Frontend UX, tương tác người dùng
- Workload: Cao (3000 dòng code)
- Focus: UI/UX, user experience, interactions
- Modules: 3-4 UserControls chính

**Shared Responsibility**:
- Services layer (cả 2 sử dụng chung)
- Models layer (cả 2 sử dụng chung)
- Code quality và testing
- Documentation

### 10.2. Yếu tố thành công

**Technical**:
- Hiểu rõ requirement và architecture
- Follow best practices
- Write clean, maintainable code
- Test thoroughly

**Teamwork**:
- Communication hiệu quả
- Support nhau khi cần
- Code review nghiêm túc
- Respect timeline

**Process**:
- Follow sprint planning
- Daily standup
- Regular commits
- Proper documentation

### 10.3. Lời khuyên cuối

1. **Đừng panic**: Mọi vấn đề đều có giải pháp
2. **Ask for help**: Đừng struggle một mình quá lâu
3. **Stay organized**: Quản lý time và tasks tốt
4. **Test often**: Đừng để bug tích lũy
5. **Have fun**: Enjoy the coding process!

---

**Chúc cả nhóm thành công!**

*Tài liệu này được tạo ngày: 9 tháng 11 năm 2025*  
*Version: 1.0*  
*Tác giả: Team Social Media Management*

---

*Ghi chú: Tài liệu này là hướng dẫn chi tiết để phân công công việc. Mọi thắc mắc vui lòng tham khảo lại tài liệu hoặc liên hệ với instructor.*
