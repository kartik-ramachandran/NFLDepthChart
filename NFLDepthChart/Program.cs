using Microsoft.OpenApi.Models;
using NFLDepthChart.Business;
using NFLDepthChart.Business.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IDepthChartAction, DepthChartAction>();
builder.Services.AddSwaggerGen(c => {
	c.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "NFL Depth CHart API",
		Version = "v1",
		Description = "NFL Depth Chart",
		Contact = new OpenApiContact
		{
			Name = "Kartik Ramachandran",
			Email = string.Empty			
		},
	});
});
builder.Services.AddControllers(
options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();

app.UseSwaggerUI(c =>
{
	c.SwaggerEndpoint("/swagger/v1/swagger.json", "NFL Depth Chart API V1");

	// To serve SwaggerUI at application's root page, set the RoutePrefix property to an empty string.
	c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
