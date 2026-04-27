# Youtube: [Link](https://www.youtube.com/watch?v=NThJUVtw0Gs&list=PLRLJQuuRRcFnwlQxGeVSVv-z_5tFwAh0j)

# 1. Controller và action method

## Nội dung chính:
Giải thích về khái niệm, vai trò của Controller và Action Method trong ASP.NET MVC.

* **Controller là gì?**  Là các lớp chịu trách nhiệm tiếp nhận và xử lý các HTTP request từ phía client gửi lên.
  * Trong ASP.NET, một Controller thường kế thừa từ lớp `Controller` (hoặc `ControllerBase`).
* **Action Method:**
  * Là các hàm có quyền truy cập `public` nằm bên trong Controller.
  * Mỗi request tới ứng dụng sẽ được điều hướng tới một Action Method cụ thể để xử lý.
* **Cơ chế Routing cơ bản:**
  * Mặc định, URL được mapping theo cấu trúc: `Tên_Controller/Tên_Action_Method`. 
  * Ví dụ: URL `/Home/Privacy` sẽ gọi tới hàm `Privacy()` nằm trong `HomeController`.
* **Lifetime của Controller & Dependency Injection:**
  * Mỗi khi có một request gửi lên, hệ thống sẽ tự động khởi tạo một object Controller **mới hoàn toàn**.
  * Có thể inject các service (như `ILogger`, `IRepository`) vào Controller thông qua constructor.
  * Sự khác biệt giữa các kiểu đăng ký DI:
    * `AddTransient`: Tạo mới một object mỗi lần service được gọi.
    * `AddScoped`: Tạo mới một object cho mỗi một HTTP Request (dùng chung trong cùng 1 request).
    * `AddSingleton`: Khởi tạo object một lần duy nhất và dùng chung cho toàn bộ ứng dụng.
* **Truyền tham số & Kiểu trả về:**
  * Action method có thể nhận tham số (ví dụ truyền qua Query String trên URL). ASP.NET tự động ép kiểu (convert) dữ liệu (như từ chuỗi sang `int`).
  * Action Method thường trả về kiểu `IActionResult`. Có thể dùng các hàm có sẵn như `View()` (trả về giao diện), `Content()` (trả về chuỗi văn bản), v.v. Nếu trả về kiểu nguyên thủy (như `string`, `int`), ASP.NET sẽ tự gọi hàm `ToString()` để in ra kết quả.

---

# 2. Các Attribute thường dùng

Các Attribute (Thuộc tính) phổ biến trong ASP.NET để quản lý hành vi của Controller và Action Method.

* **Thuộc tính giới hạn truy cập:**
  * `[NonAction]`: Đặt trước một hàm public để báo cho ASP.NET biết đây **không phải** là một Action Method. Hệ thống sẽ không ánh xạ URL nào vào hàm này, người dùng không thể gọi nó qua trình duyệt.
  * `[NonController]`: Đặt trước một class để đánh dấu nó không phải là Controller. Thuộc tính này có tính kế thừa, tức là mọi class con kế thừa class này cũng sẽ không được coi là Controller.
* **Nhóm thuộc tính HTTP Methods (Động từ HTTP):**
  * Bao gồm: `[HttpGet]`, `[HttpPost]`, `[HttpPut]`, `[HttpDelete]`, v.v.
  * Tác dụng: Giới hạn một Action Method chỉ được phép xử lý một loại Request nhất định. 
  * Có thể map cùng một URL tới nhiều hàm khác nhau, miễn là chúng được phân biệt bởi các HTTP Method khác nhau (ví dụ: GET để lấy danh sách, POST để thêm mới).
* **Nhóm thuộc tính "From" (Xác định nguồn lấy tham số):**
  * Được đặt trước các tham số của hàm Action Method để chỉ định rõ nguồn dữ liệu mà ASP.NET cần lấy:
    * `[FromQuery]`: Bắt buộc lấy giá trị từ Query String (đoạn URL sau dấu `?`).
    * `[FromForm]`: Lấy giá trị từ dữ liệu Form gửi lên (Form Data).
    * `[FromHeader]`: Lấy dữ liệu từ HTTP Header (Rất hay dùng để truyền API Key).
    * `[FromServices]`: Lấy một instance/service trực tiếp từ bộ chứa Dependency Injection.
    * `[FromBody]`: Lấy dữ liệu từ phần Body của Request.
* **Thuộc tính `[Route]`:**
  * Cho phép định nghĩa một đường dẫn (URL) tùy chỉnh cho Controller hoặc Action Method thay vì dùng tên mặc định. 
  * Ví dụ: Khai báo `[Route("api/users")]` sẽ ép người dùng phải gọi đúng đường dẫn đó thay vì đường dẫn cơ bản mặc định.