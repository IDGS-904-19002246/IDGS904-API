//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddRazorPages();
//var app = builder.Build();
//if (!app.Environment.IsDevelopment()){app.UseExceptionHandler("/Error");app.UseHsts();}
//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseAuthorization();
//app.MapRazorPages();
//app.Run();

//........................................................................................................
using IDGS904_API;
var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
startup.Configure(app, app.Lifetime);
if (app.Environment.IsDevelopment()){app.UseSwagger();app.UseSwaggerUI();}
app.UseHttpsRedirection();app.UseAuthorization();app.MapControllers();app.UseCors();app.Run();