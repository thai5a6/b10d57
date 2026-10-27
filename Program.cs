using Radzen;
using TaiSanHV.Components;
using TaiSanHV.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();               // Dialog, Notification, Tooltip, ContextMenu...
builder.Services.AddSingleton<AccountService>();      // Lưu trong bộ nhớ (demo). Sau này thay bằng EF Core + SQL Server

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
