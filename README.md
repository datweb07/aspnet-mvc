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

---

# 3. Routing
Giải thích về cơ chế **Routing (Điều hướng)** trong ASP.NET MVC, có nhiệm vụ ánh xạ (map) một URL (đường dẫn web) được gửi từ trình duyệt tới một Action Method cụ thể trong Controller.

Nếu ASP.NET không tìm thấy bất kỳ Action Method nào khớp với URL, nó sẽ trả về lỗi **404 (Not Found)**.

Có 2 phương pháp chính để định nghĩa Routing trong ASP.NET:

### 1. Conventional Routing (Khai báo tập trung tại Startup/Program.cs)
Đây là cách thiết lập các quy tắc chung cho toàn bộ ứng dụng khi khởi động chương trình.
* Sử dụng hai lệnh chính trong file cấu hình:
  * `app.UseRouting()`: Đăng ký middleware routing để bắt và phân tích URL.
  * `app.MapControllerRoute()`: Đặt ra các quy tắc khớp URL (pattern matching).
* **Quy tắc mặc định:** `{controller=Home}/{action=Index}/{id?}`
  * Nếu không gõ URL gì cả, hệ thống mặc định gọi `HomeController` và hàm `Index`.
  * `id?` có dấu `?` nghĩa là tham số này không bắt buộc (optional).
* **Tạo quy tắc tùy chỉnh:** Có thể map những đường dẫn ngắn gọn hơn. 
  * *Ví dụ:* URL dạng `/p/123` thay vì `/product/details/123` để tối ưu SEO. Ta có thể thêm một route mới đặt tên pattern là `/p/{id}` và chỉ định nó trỏ cứng về `controller="Product"` và `action="Details"`.
* **Lưu ý:** Các Route được khai báo sẽ ưu tiên chạy theo **thứ tự từ trên xuống dưới**. Route nào khai báo trước sẽ được dùng để khớp trước.

### 2. Attribute Routing (Khai báo bằng Thuộc tính trực tiếp)
Phương pháp này dùng các thẻ attribute đặt ngay trên đầu Action Method, phù hợp cho các dự án lớn, phức tạp để tránh làm file cấu hình chung bị quá tải.
* Sử dụng attribute `[Route("...")]`.
* *Ví dụ:* Thay vì thiết lập ở `Program.cs`, ta có thể đặt trực tiếp `[Route("p/{id}")]` ngay trên hàm `Details()` của `ProductController`.
* **Nhiều Route cho 1 hàm:** Có thể gắn nhiều thẻ `[Route]` khác nhau cho cùng một hàm (ví dụ: `[Route("p/{id}")]` và `[Route("product/{id}")]` thì cả hai link này đều gọi chung vào một hàm).
* **Ràng buộc kiểu dữ liệu (Route Constraints):** * Có thể chỉ định kiểu dữ liệu ngay trên tham số URL để phân biệt hàm gọi.
  * *Ví dụ:* `[Route("product/{id:int}")]` sẽ chỉ nhận tham số là số nguyên, trong khi `[Route("product/{name}")]` sẽ gọi một hàm khác nhận tham số dạng chuỗi (string). Nhờ đó, URL `product/123` và `product/nuoc-hoa` sẽ trỏ đến hai hàm xử lý khác nhau.