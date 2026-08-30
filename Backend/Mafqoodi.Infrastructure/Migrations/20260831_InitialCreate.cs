using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mafqoodi.Infrastructure.Migrations;

[Migration("202608310001_InitialCreate")]
public partial class InitialCreate : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable("Users", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), Name = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
            Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false), PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
            ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true), Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
            AccountType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false), Role = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            IsBanned = table.Column<bool>(type: "bit", nullable: false), IsPhoneVerified = table.Column<bool>(type: "bit", nullable: false),
            PasswordHash = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Users", x => x.Id));
        migrationBuilder.CreateIndex("IX_Users_Email", "Users", "Email", unique: true);

        migrationBuilder.CreateTable("Organizations", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            Description = table.Column<string>(type: "nvarchar(max)", nullable: true), Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
            Address = table.Column<string>(type: "nvarchar(max)", nullable: true), LogoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
            IsActive = table.Column<bool>(type: "bit", nullable: false), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => table.PrimaryKey("PK_Organizations", x => x.Id));
        migrationBuilder.CreateIndex("IX_Organizations_Name", "Organizations", "Name");

        migrationBuilder.CreateTable("Reports", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
            Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false), LocationName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
            Latitude = table.Column<double>(type: "float", nullable: true), Longitude = table.Column<double>(type: "float", nullable: true), UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            PublisherPhone = table.Column<string>(type: "nvarchar(max)", nullable: true), PublisherAccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
            ReportType = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false), Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
            CustomCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true), RewardAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
            RewardCurrency = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            Status = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false), AdminStatus = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
            ImageData = table.Column<string>(type: "nvarchar(max)", nullable: true), Base64Image = table.Column<string>(type: "nvarchar(max)", nullable: true)
        }, constraints: table => { table.PrimaryKey("PK_Reports", x => x.Id); table.ForeignKey("FK_Reports_Users", x => x.UserId, "Users", "Id", onDelete: ReferentialAction.Restrict); });
        migrationBuilder.CreateIndex("IX_Reports_Type_Category_Status", "Reports", new[] { "ReportType", "Category", "Status" });
        migrationBuilder.CreateIndex("IX_Reports_Location", "Reports", new[] { "Latitude", "Longitude" });
        migrationBuilder.CreateIndex("IX_Reports_CreatedAt", "Reports", "CreatedAt");

        migrationBuilder.CreateTable("ReportFlags", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => { table.PrimaryKey("PK_ReportFlags", x => x.Id); table.ForeignKey("FK_ReportFlags_Reports", x => x.ReportId, "Reports", "Id", onDelete: ReferentialAction.Cascade); });
        migrationBuilder.CreateIndex("IX_ReportFlags_Report_User", "ReportFlags", new[] { "ReportId", "UserId" }, unique: true);

        migrationBuilder.CreateTable("SupportChats", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => { table.PrimaryKey("PK_SupportChats", x => x.Id); table.ForeignKey("FK_SupportChats_Users", x => x.UserId, "Users", "Id", onDelete: ReferentialAction.Restrict); });
        migrationBuilder.CreateIndex("IX_SupportChats_User", "SupportChats", "UserId", unique: true);

        migrationBuilder.CreateTable("SupportMessages", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false), Body = table.Column<string>(type: "nvarchar(5000)", maxLength: 5000, nullable: false),
            IsRead = table.Column<bool>(type: "bit", nullable: false), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => { table.PrimaryKey("PK_SupportMessages", x => x.Id); table.ForeignKey("FK_SupportMessages_Chats", x => x.ChatId, "SupportChats", "Id", onDelete: ReferentialAction.Cascade); });
        migrationBuilder.CreateIndex("IX_SupportMessages_Chat_CreatedAt", "SupportMessages", new[] { "ChatId", "CreatedAt" });

        migrationBuilder.CreateTable("Notifications", columns: table => new
        {
            Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false), UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false), Body = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
            IsRead = table.Column<bool>(type: "bit", nullable: false), CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
        }, constraints: table => { table.PrimaryKey("PK_Notifications", x => x.Id); table.ForeignKey("FK_Notifications_Users", x => x.UserId, "Users", "Id", onDelete: ReferentialAction.Cascade); });
        migrationBuilder.CreateIndex("IX_Notifications_User_Read_Created", "Notifications", new[] { "UserId", "IsRead", "CreatedAt" });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable("Notifications"); migrationBuilder.DropTable("SupportMessages"); migrationBuilder.DropTable("SupportChats");
        migrationBuilder.DropTable("ReportFlags"); migrationBuilder.DropTable("Reports"); migrationBuilder.DropTable("Organizations"); migrationBuilder.DropTable("Users");
    }

    protected override void BuildTargetModel(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder) { }
}
