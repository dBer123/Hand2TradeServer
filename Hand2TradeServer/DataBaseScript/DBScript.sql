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
    sumRanks FLOAT NOT NULL,
    countRanked INT NOT NULL,
    bearthDate DATETIME NOT NULL,
    adress NVARCHAR(255) NOT NULL,
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


CREATE TABLE TradeChats(
    chatID INT IDENTITY Primary Key NOT NULL ,
    itemID INT NOT NULL FOREIGN KEY REFERENCES Items(itemID),
    buyerID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    sellerID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
) 

CREATE TABLE TextMessages(
    messageID INT IDENTITY Primary Key NOT NULL,
    chatID INT NOT NULL FOREIGN KEY REFERENCES TradeChats(chatID),
    senderID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
    textMessage VARCHAR(255) NOT NULL,
	sentTime DATETIME NOT NULL
) 





CREATE TABLE MonthlyReports(
    monthlyReportID INT IDENTITY Primary Key NOT NULL,
    dateOfMonth DateTime NOT NULL,
    newSubs INT NOT NULL,
    itemsTraded INT NOT NULL,
    reportsNum INT NOT NULL
)

CREATE TABLE DailyReports(
    DailyReportID INT IDENTITY Primary Key NOT NULL,
    dayTime DateTime NOT NULL,
    newSubs INT NOT NULL,
    itemsDraded INT NOT NULL,    
    reportsNum INT NOT NULL
) 

CREATE TABLE Reports(
    reportID INT IDENTITY Primary Key NOT NULL,
	reportedUserID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
	senderID INT NOT NULL FOREIGN KEY REFERENCES Users(userID)
)

CREATE TABLE Ratings(
    ratingID INT IDENTITY Primary Key NOT NULL,
	ratedUserID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
	senderID INT NOT NULL FOREIGN KEY REFERENCES Users(userID),
	rate FLOAT NOT NULL
)
CREATE TABLE LikedItems(
    likedItemID INT IDENTITY Primary Key NOT NULL,
	itemID INT NOT NULL FOREIGN KEY REFERENCES Items(itemID),
	senderID INT NOT NULL FOREIGN KEY REFERENCES Users(userID)
)

INSERT INTO Users (email, passwrd, userName, isAdmin, coins, reports, sumRanks, countRanked, bearthDate, adress, isBlocked)
VALUES ('berdaniel04@gmail.com', 'daniel6839', 'danBer', '1', '500', '0', '0','0','2003-12-25','hailanot st. 7','0');
INSERT INTO Users (email, passwrd, userName, isAdmin, coins, reports, sumRanks, countRanked, bearthDate, adress, isBlocked)
VALUES ('danielbe4@ramon.edum.org.il', 'daniel6839', 'danBer2', '1', '500', '0', '0','0','2003-12-25','hailanot st. 7','0');