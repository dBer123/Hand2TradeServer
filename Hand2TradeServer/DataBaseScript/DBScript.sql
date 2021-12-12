Use master
Create Database Hand2TradeDB

Use Hand2TradeDB

CREATE TABLE Users(
    userID INT  IDENTITY Primary Key NOT NULL ,
    email NVARCHAR (100) NOT NULL,
    passwrd NVARCHAR(30) NOT NULL,
    userName NVARCHAR(30) NOT NULL,
    isAdmin Bit NOT NULL,
    coins INT NOT NULL,
    reports INT NOT NULL,
    sumRanks INT NOT NULL,
    countRanked INT NOT NULL,
    bearthDate DATETIME NOT NULL,
    adress NVARCHAR(255) NOT NULL,
    creditCardNumber NVARCHAR(255) NOT NULL,
    CVV NVARCHAR(255) NOT NULL, 
    creditCardValidity DATETIME NOT NULL,
    isBlocked Bit NOT NULL,
	CONSTRAINT UC_email UNIQUE(email)

) 


CREATE TABLE Items(
    itemID INT IDENTITY Primary Key NOT NULL,
    userID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    itemName NVARCHAR(30) NOT NULL,
    price INT NOT NULL,
    desrciption NVARCHAR(255) NOT NULL
)


CREATE TABLE TradeChat(
    chatID INT IDENTITY Primary Key NOT NULL ,
    itemID INT NOT NULL FOREIGN KEY REFERENCES Items(itemID),
    buyerID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    sellerID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    isTradeAgreed Bit NOT NULL
) 

CREATE TABLE TextMessage(
    messageID INT IDENTITY Primary Key NOT NULL,
    chatID INT NOT NULL FOREIGN KEY REFERENCES Items(itemID),
    senderID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    textMessage VARCHAR(255) NOT NULL
) 


CREATE TABLE Loan(
    LoanID INT IDENTITY Primary Key NOT NULL,
    loanerID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    coinsLoaned INT NOT NULL,
    debt INT NOT NULL,
    isDebtPaid Bit NOT NULL,
    paymentDate DATETIME NOT NULL
) 

CREATE TABLE DailyRepor(
    DailyReportReportID INT IDENTITY Primary Key NOT NULL,
    dateOfDay DateTime NOT NULL,
    newSubs INT NOT NULL,
    itemsTraded INT NOT NULL,
    loansTaken INT NOT NULL,
    loansDeptPaid INT NOT NULL,
    reportsNum INT NOT NULL
)

CREATE TABLE MonthlyReport(
    MonthlyReportID INT IDENTITY Primary Key NOT NULL,
    dateOfMonth DateTime NOT NULL,
    newSubs INT NOT NULL,
    itemsTraded INT NOT NULL,
    loansTaken INT NOT NULL,
    loansDeptPaid INT NOT NULL,
    reportsNum INT NOT NULL
)

CREATE TABLE HourlyReport(
    HourlyReportID INT IDENTITY Primary Key NOT NULL,
    hourTime DateTime NOT NULL,
    newSubs INT NOT NULL,
    itemsDraded INT NOT NULL,
    loansTaken INT NOT NULL,
    loansDeptPaid INT NOT NULL,
    reportsNum INT NOT NULL
) 

INSERT INTO Users (email, passwrd, userName, isAdmin, coins, reports, sumRanks, countRanked, totalRank, bearthDate, adress, creditCardNumber, CVV, creditCardValidity, isBlocked)
VALUES ('berdaniel04@gmail.com', 'daniel6839', 'danBer', '1', '0', '0', '0','0','0','2003-12-25','hailanot st. 7', '12313', '111','2023-12-25','0');