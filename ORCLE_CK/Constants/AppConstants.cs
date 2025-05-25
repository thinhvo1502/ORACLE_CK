using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORCLE_CK.Constants
{
    public static class AppConstants
    {
        public const string APP_NAME = "E-Learning Management System";
        public const string APP_VERSION = "1.0.0";
        public const string COMPANY_NAME = "Your Company";

        // UI Constants
        public const int FORM_PADDING = 20;
        public const int CONTROL_SPACING = 10;
        public const int BUTTON_HEIGHT = 30;
        public const int TEXTBOX_HEIGHT = 23;

        // Database Constants
        public const int DEFAULT_PAGE_SIZE = 50;
        public const int MAX_PAGE_SIZE = 1000;

        // File Constants
        public const string LOG_FILE_EXTENSION = ".txt";
        public const string CONFIG_FILE_EXTENSION = ".config";

        // Date Formats
        public const string DATE_FORMAT = "dd/MM/yyyy";
        public const string DATETIME_FORMAT = "dd/MM/yyyy HH:mm:ss";
        public const string TIME_FORMAT = "HH:mm:ss";
    }

    public static class MessageConstants
    {
        // Success Messages
        public const string USER_CREATED_SUCCESS = "Thêm người dùng thành công!";
        public const string USER_UPDATED_SUCCESS = "Cập nhật người dùng thành công!";
        public const string USER_DELETED_SUCCESS = "Xóa người dùng thành công!";
        public const string COURSE_CREATED_SUCCESS = "Thêm khóa học thành công!";
        public const string COURSE_UPDATED_SUCCESS = "Cập nhật khóa học thành công!";
        public const string COURSE_DELETED_SUCCESS = "Xóa khóa học thành công!";
        public const string PASSWORD_CHANGED_SUCCESS = "Đổi mật khẩu thành công!";

        // Error Messages
        public const string LOGIN_FAILED = "Tên đăng nhập hoặc mật khẩu không đúng!";
        public const string ACCESS_DENIED = "Bạn không có quyền truy cập chức năng này!";
        public const string DATABASE_ERROR = "Lỗi kết nối cơ sở dữ liệu!";
        public const string VALIDATION_ERROR = "Dữ liệu nhập vào không hợp lệ!";
        public const string DUPLICATE_USERNAME = "Tên đăng nhập đã tồn tại!";
        public const string DUPLICATE_EMAIL = "Email đã tồn tại!";
        public const string WEAK_PASSWORD = "Mật khẩu quá yếu!";

        // Warning Messages
        public const string UNSAVED_CHANGES = "Bạn có thay đổi chưa được lưu. Bạn có muốn tiếp tục?";
        public const string DELETE_CONFIRMATION = "Bạn có chắc chắn muốn xóa?";
        public const string LOGOUT_CONFIRMATION = "Bạn có chắc chắn muốn đăng xuất?";
        public const string EXIT_CONFIRMATION = "Bạn có chắc chắn muốn thoát?";

        // Info Messages
        public const string LOADING_DATA = "Đang tải dữ liệu...";
        public const string SAVING_DATA = "Đang lưu dữ liệu...";
        public const string PROCESSING = "Đang xử lý...";
        public const string NO_DATA_FOUND = "Không tìm thấy dữ liệu!";
        public const string FEATURE_UNDER_DEVELOPMENT = "Chức năng đang được phát triển!";
    }
}
