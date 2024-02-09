using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockApi;
using StockApi.Models;
using StockApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(
    opt => opt
    .UseSqlServer(builder.Configuration.GetConnectionString("StockConnection")));
builder.Services.AddScoped<JWTServices>();
// defining our IdentityCore Service
builder.Services.AddIdentityCore<StockUser>(options =>
{
    // password configuration
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    // for email confirmation
    options.SignIn.RequireConfirmedEmail = false;
}).AddRoles<IdentityRole>() // be able to add roles
    .AddRoleManager<RoleManager<IdentityRole>>() // be able to make use of RoleManager
    .AddEntityFrameworkStores<ApplicationDbContext>() // providing our context
    .AddSignInManager<SignInManager<StockUser>>() // make use of Signin manager
    .AddUserManager<UserManager<StockUser>>() // make use of UserManager to create users
    .AddDefaultTokenProviders(); 
// be able to authenticate users using JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // validate the token based on the key we have provided inside appsettings.development.json JWT:Key
            ValidateIssuerSigningKey = true,
            // the issuer singning key based on JWT:Key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
            // the issuer which in here is the api project url we are using
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            // validate the issuer (who ever is issuing the JWT)
            ValidateIssuer = true,
            // don't validate audience (angular side)
            ValidateAudience = false,
            //ValidateLifetime = true,
            //ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSignalR();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add Service agular and SignalR
builder.Services.AddCors(
    option=>option.AddPolicy("AllowAllHeaders",
    builder =>
    {
        builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
    }));
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});

builder.Services.AddControllers();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

//app.UseAuthentication();
app.UseAuthorization();
//added
app.UseCors("AllowAllHeaders");
app.MapControllers();
app.MapHub<StockHub>("/stockHub");
app.Run();
