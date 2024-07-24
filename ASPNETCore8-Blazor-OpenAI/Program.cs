using ASPNETCore8.Blazor.OpenAI.Components.Player.Data;
using ASPNETCore8.Blazor.OpenAI.Configurations;
using ASPNETCore8.Blazor.OpenAI.Data;
using ASPNETCore8.Blazor.OpenAI.Services.Markdown;
using ASPNETCore8.Blazor.OpenAI.Services.OpenAI;
using ASPNETCore8_Blazor_OpenAI.Components;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));

builder.Services.AddScoped<IVideoRepository, VideoRepository>();
builder.Services.AddScoped<IMarkdownService, MarkdownService>();
builder.Services.AddScoped<IGptService, GptService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseDbMigrationHelper();
app.Run();
