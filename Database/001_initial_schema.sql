-- مخطط SQL Server مطابق لكيانات Mafqoodi الحالية.
CREATE TABLE Users (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_Users PRIMARY KEY,
    Name nvarchar(120) NOT NULL,
    Email nvarchar(256) NOT NULL CONSTRAINT UQ_Users_Email UNIQUE,
    PhoneNumber nvarchar(30) NULL,
    ProfileImageUrl nvarchar(2048) NULL,
    Base64Image nvarchar(max) NULL,
    AccountType nvarchar(30) NOT NULL,
    Role nvarchar(30) NOT NULL,
    IsBanned bit NOT NULL,
    IsPhoneVerified bit NOT NULL,
    PasswordHash nvarchar(512) NOT NULL,
    CreatedAt datetime2 NOT NULL
);

CREATE TABLE Reports (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_Reports PRIMARY KEY,
    Title nvarchar(200) NOT NULL,
    Description nvarchar(4000) NOT NULL,
    LocationName nvarchar(300) NOT NULL,
    Latitude float NULL,
    Longitude float NULL,
    UserId uniqueidentifier NOT NULL,
    PublisherPhone nvarchar(30) NULL,
    PublisherAccountType nvarchar(30) NOT NULL,
    ReportType nvarchar(30) NOT NULL,
    Category nvarchar(100) NULL,
    CustomCategoryName nvarchar(200) NULL,
    RewardAmount decimal(18,2) NULL,
    RewardCurrency nvarchar(10) NULL,
    CreatedAt datetime2 NOT NULL,
    Status nvarchar(30) NOT NULL,
    AdminStatus nvarchar(30) NOT NULL,
    ImageData nvarchar(max) NULL,
    Base64Image nvarchar(max) NULL,
    CONSTRAINT FK_Reports_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);
CREATE INDEX IX_Reports_Type_Category_Status ON Reports(ReportType, Category, Status);
CREATE INDEX IX_Reports_Location ON Reports(Latitude, Longitude);
CREATE INDEX IX_Reports_CreatedAt ON Reports(CreatedAt);

CREATE TABLE ReportFlags (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_ReportFlags PRIMARY KEY,
    ReportId uniqueidentifier NOT NULL,
    UserId uniqueidentifier NOT NULL,
    CreatedAt datetime2 NOT NULL,
    CONSTRAINT FK_ReportFlags_Reports FOREIGN KEY (ReportId) REFERENCES Reports(Id) ON DELETE CASCADE,
    CONSTRAINT UQ_ReportFlags_Report_User UNIQUE (ReportId, UserId)
);

CREATE TABLE Organizations (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_Organizations PRIMARY KEY,
    Name nvarchar(200) NOT NULL,
    Description nvarchar(2000) NULL,
    Phone nvarchar(30) NULL,
    Address nvarchar(500) NULL,
    LogoUrl nvarchar(2048) NULL,
    IsActive bit NOT NULL,
    CreatedAt datetime2 NOT NULL
);
CREATE INDEX IX_Organizations_Name ON Organizations(Name);

CREATE TABLE SupportChats (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_SupportChats PRIMARY KEY,
    UserId uniqueidentifier NOT NULL CONSTRAINT UQ_SupportChats_User UNIQUE,
    CreatedAt datetime2 NOT NULL,
    CONSTRAINT FK_SupportChats_Users FOREIGN KEY (UserId) REFERENCES Users(Id)
);

CREATE TABLE SupportMessages (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_SupportMessages PRIMARY KEY,
    ChatId uniqueidentifier NOT NULL,
    SenderId uniqueidentifier NOT NULL,
    Body nvarchar(5000) NOT NULL,
    IsRead bit NOT NULL,
    CreatedAt datetime2 NOT NULL,
    CONSTRAINT FK_SupportMessages_Chats FOREIGN KEY (ChatId) REFERENCES SupportChats(Id) ON DELETE CASCADE
);
CREATE INDEX IX_SupportMessages_Chat_CreatedAt ON SupportMessages(ChatId, CreatedAt);

CREATE TABLE Notifications (
    Id uniqueidentifier NOT NULL CONSTRAINT PK_Notifications PRIMARY KEY,
    UserId uniqueidentifier NOT NULL,
    Title nvarchar(200) NOT NULL,
    Body nvarchar(4000) NOT NULL,
    IsRead bit NOT NULL,
    CreatedAt datetime2 NOT NULL,
    CONSTRAINT FK_Notifications_Users FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
CREATE INDEX IX_Notifications_User_Read_Created ON Notifications(UserId, IsRead, CreatedAt);
