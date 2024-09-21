using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;

namespace Identity.Api
{
    public static class LoggingConfiguration
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (context, configuration) =>
        {
            var env = context.HostingEnvironment;

            // افزودن داده‌های کانتکست لاگ (مثل اطلاعات درخواست‌ها) به لاگ‌ها
            configuration.Enrich.FromLogContext()

                // افزودن نام اپلیکیشن به هر لاگ
                .Enrich.WithProperty("ApplicationName", env.ApplicationName)

                //.Enrich.WithProperty("UserId", env.ApplicationName)

                // افزودن نام محیط اجرایی (مثلاً Development یا Production)
                .Enrich.WithProperty("Environment", env.EnvironmentName)

                // افزودن نام سرور یا ماشینی که برنامه روی آن اجرا می‌شود
                .Enrich.WithMachineName()

                // افزودن شناسه یکتا برای درخواست‌ها جهت پیگیری و ردیابی
                .Enrich.WithCorrelationId()

                // افزودن جزئیات کامل از خطاها (Exception)
                .Enrich.WithExceptionDetails()

                // افزودن آدرس آی‌پی کاربر (مشتری)
                .Enrich.WithClientIp()

                // افزودن شناسه ترد (Thread) برای ردیابی پردازش‌های موازی
                .Enrich.WithThreadId() // اضافه شده

                // نوشتن لاگ‌ها در کنسول با فرمت JSON ساختاریافته
                .WriteTo.Console(new RenderedCompactJsonFormatter()) // بهبود نمایش لاگ در کنسول

                // ذخیره لاگ‌ها در فایل و ایجاد یک فایل جدید برای هر روز
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // ذخیره در فایل

                // تنظیم سطح لاگ برای لاگ‌های مربوط به Microsoft روی "Warning" تا فقط اخطارها و خطاها ثبت شوند
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) // فیلتر لاگ‌های Microsoft

                // تنظیم حداقل سطح لاگ روی "Information" تا لاگ‌های اطلاعاتی و بالاتر ثبت شوند
                .MinimumLevel.Information();
        };

    }
}
