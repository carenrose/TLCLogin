DROP TABLE [Admin]
DROP TABLE [AreaOfAssistance]
DROP TABLE [Campus]
DROP TABLE [CountryOfOrigin]
DROP TABLE [Course]
DROP TABLE [CourseCategory]
DROP TABLE [LoginHistory]
DROP TABLE [Misc_OtherCredentials]
DROP TABLE [Misc_SurveyFrequency]
DROP TABLE [NativeLanguage]
DROP TABLE [ProgramsOfStudy]
DROP TABLE [Student]
DROP TABLE [Student_StudentService]
DROP TABLE [Student_StudentService_Past]
DROP TABLE [StudentServiceProgram]
DROP TABLE [Survey_HearAbout]


/*
CREATE TABLE Misc_SurveyFrequency (
  FrequencyPercent INT DEFAULT 0
)
*/

CREATE TABLE Credentials (
  Username NVARCHAR(255) NOT NULL,
  Password NVARCHAR(255) NULL,
  WinLogin BIT           NOT NULL DEFAULT 1,
  IsAdmin  BIT           NOT NULL DEFAULT 0,
  Enabled  BIT           NOT NULL DEFAULT 0,
  CONSTRAINT PK_Credentials PRIMARY KEY (Username)
  --CONSTRAINT CHK_Cred_Passwd CHECK (WinLogin = 1 OR Password IS NOT NULL)     -- not supported by sql server ce
)

CREATE TABLE AreaOfAssistance (
  AreaID               INT           NOT NULL IDENTITY,
  AreaName             NVARCHAR(255) NULL,
  DefaultLogoffMinutes INT           NULL     DEFAULT 60,
  SkipsCourseSelection BIT           NULL     DEFAULT 0,
  CONSTRAINT PK_AoA PRIMARY KEY (AreaID)
)


CREATE TABLE LKP_Campus                ( ID INT NOT NULL IDENTITY, Name NVARCHAR(255) NOT NULL, Disabled BIT NOT NULL DEFAULT 0, CONSTRAINT PK_LKP_Campus      PRIMARY KEY (ID) )
CREATE TABLE LKP_CountryOfOrigin       ( ID INT NOT NULL IDENTITY, Name NVARCHAR(255) NOT NULL, Disabled BIT NOT NULL DEFAULT 0, CONSTRAINT PK_LKP_Country     PRIMARY KEY (ID) )
CREATE TABLE LKP_NativeLanguage        ( ID INT NOT NULL IDENTITY, Name NVARCHAR(255) NOT NULL, Disabled BIT NOT NULL DEFAULT 0, CONSTRAINT PK_LKP_Language    PRIMARY KEY (ID) )
CREATE TABLE LKP_SurveyReferrer        ( ID INT NOT NULL IDENTITY, Name NVARCHAR(255) NOT NULL, Disabled BIT NOT NULL DEFAULT 0, CONSTRAINT PK_LKP_Referrer    PRIMARY KEY (ID) )
CREATE TABLE LKP_StudentServiceProgram ( ID INT NOT NULL IDENTITY, Name NVARCHAR(255) NOT NULL, Disabled BIT NOT NULL DEFAULT 0, CONSTRAINT PK_LKP_StuServProg PRIMARY KEY (ID) )

CREATE TABLE LKP_ProgramOfStudy (
  ID       INT           NOT NULL IDENTITY,
  Code     NVARCHAR(255) NOT NULL,
  Name     NVARCHAR(255) NOT NULL,
  Disabled BIT           NOT NULL DEFAULT 0,
  CONSTRAINT PK_LKP_ProgramOfStudy PRIMARY KEY (ID)
)
CREATE TABLE LKP_CourseCategory (
  Code     NVARCHAR(4)   NOT NULL,
  Name     NVARCHAR(255) NOT NULL,
  Disabled BIT           NOT NULL DEFAULT 0,
  CONSTRAINT PK_LKP_CourseCategory PRIMARY KEY (Code)
)
CREATE TABLE LKP_Course (
  ID           INT           NOT NULL IDENTITY,
  FK_Category  NVARCHAR(4)   NOT NULL,
  CourseNumber INT           NOT NULL,
  CourseTitle  NVARCHAR(255) NULL,
  Disabled     BIT           NOT NULL DEFAULT 0,
  CONSTRAINT PK_LKP_Course PRIMARY KEY (ID),
  CONSTRAINT FK_Course_Category FOREIGN KEY (FK_Category) REFERENCES LKP_CourseCategory(Code),
  CONSTRAINT UC_Course UNIQUE (FK_Category, CourseNumber)
)


CREATE TABLE Student (
  StudentID          INT           NOT NULL,
  FirstName          NVARCHAR(255) NOT NULL,
  LastName           NVARCHAR(255) NOT NULL,
  FK_NativeLanguage  INT           NULL,
  FK_CountryOfOrigin INT           NULL,
  FK_ProgramOfStudy  INT           NULL,
  CONSTRAINT PK_Student PRIMARY KEY (StudentID),
  CONSTRAINT FK_Student_Language FOREIGN KEY (FK_NativeLanguage)  REFERENCES LKP_NativeLanguage (ID),
  CONSTRAINT FK_Student_Country  FOREIGN KEY (FK_CountryOfOrigin) REFERENCES LKP_CountryOfOrigin(ID),
  CONSTRAINT FK_Student_Program  FOREIGN KEY (FK_ProgramOfStudy)  REFERENCES LKP_ProgramOfStudy (ID)
)

CREATE TABLE Login (
  ID                  INT      NOT NULL IDENTITY,
  FK_Student          INT      NOT NULL,
  FK_Campus           INT      NOT NULL,
  FK_AreaOfAssistance INT      NOT NULL,
  FK_Course           INT      NULL,
  LogInTime           DATETIME NOT NULL,
  LogOutTime          DATETIME NULL,
  SurveyRating        INT      NULL,
  FK_SurveyReferrer   INT      NULL,
  CONSTRAINT PK_Login PRIMARY KEY (ID),
  CONSTRAINT FK_Login_Student          FOREIGN KEY (FK_Student)          REFERENCES Student           (StudentID),
  CONSTRAINT FK_Login_Campus           FOREIGN KEY (FK_Campus)           REFERENCES LKP_Campus        (ID),
  CONSTRAINT FK_Login_AoA              FOREIGN KEY (FK_AreaOfAssistance) REFERENCES AreaOfAssistance  (AreaID),
  CONSTRAINT FK_Login_Course           FOREIGN KEY (FK_Course)           REFERENCES LKP_Course        (ID),
  CONSTRAINT FK_Login_Survey_HearAbout FOREIGN KEY (FK_SurveyReferrer)   REFERENCES LKP_SurveyReferrer(ID)
)


CREATE TABLE REL_Student_StudentService (
  ID         INT NOT NULL IDENTITY,
  FK_Student INT NOT NULL,
  FK_Program INT NOT NULL,
  IsCurrent  BIT NOT NULL DEFAULT 1,
  CONSTRAINT PK_REL_StuStuSrvc PRIMARY KEY (ID),
  CONSTRAINT UC_Course UNIQUE (FK_Student, FK_Program, IsCurrent),
  CONSTRAINT FK_REL_StudentService FOREIGN KEY (FK_Student) REFERENCES LKP_StudentServiceProgram(ID),
  CONSTRAINT FK_REL_Student        FOREIGN KEY (FK_Program) REFERENCES Student(StudentID)
)
