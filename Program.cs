namespace learn_asp.net_mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            /*
            AddTransient (Mỗi lần gọi, tạo mới 1 cái):
            Đặc điểm: Cứ mỗi class hoặc mỗi chỗ nào gọi đến IRepository, nó sẽ tạo ra một đối tượng MyRepository riêng biệt, không ai chung chạ với ai.
            Khi nào dùng: Dành cho các service nhẹ, thực hiện xong tác vụ là bỏ đi, không cần lưu trữ trạng thái (stateless) giữa các lần gọi. 
            */
            builder.Services.AddTransient<IRepository>(services => new MyRepository(services.GetRequiredService<ILogger<MyRepository>>()));

            /*
            AddScoped (Mỗi một HTTP Request, tạo mới 1 cái):
            Đặc điểm: Khi người dùng gửi 1 request (ví dụ click vào 1 trang web), hệ thống tạo 1 instance và dùng chung instance đó cho tất cả các class chạy trong request đó. Sang request khác sẽ tạo cái mới.
            Khi nào dùng: Rất phổ biến khi làm việc với Database (như DbContext của Entity Framework), để đảm bảo cùng 1 luồng xử lý sẽ làm việc trên cùng 1 kết nối database. 
            */
            //builder.Services.AddScoped<IRepository>(services => new MyRepository(services.GetRequiredService<ILogger<MyRepository>>()));


            /*
            AddSingleton (Chỉ tạo 1 cái duy nhất cho toàn bộ app):
            Đặc điểm: Tạo ra 1 bản sao duy nhất khi ứng dụng chạy. Tất cả người dùng, tất cả request đều dùng chung cái này.
            Khi nào dùng: Dùng cho bộ đệm (Cache), cấu hình hệ thống (Configuration) hoặc các service tốn nhiều tài nguyên khởi tạo. 
            */
            //builder.Services.AddSingleton<IRepository>(services => new MyRepository(services.GetRequiredService<ILogger<MyRepository>>()));

            builder.Services.AddSingleton<IUserRepository>(services => new InMemoryUserRepository());

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
