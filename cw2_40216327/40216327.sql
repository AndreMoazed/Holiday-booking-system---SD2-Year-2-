DROP TABLE IF EXISTS `car_hire`;
DROP TABLE IF EXISTS `evening_meal`;
DROP TABLE IF EXISTS `breakfast`;
DROP TABLE IF EXISTS `guest`;
DROP TABLE IF EXISTS `booking`;
DROP TABLE IF EXISTS `customer`;


CREATE TABLE `customer`
(
	cust_ref INT(11) NOT NULL AUTO_INCREMENT,
	cust_lastname NCHAR(50) DEFAULT NULL,
	cust_firstname NCHAR(50) DEFAULT NULL,
	cust_address NCHAR(100) DEFAULT NULL,
	PRIMARY KEY (`cust_ref`)
);


CREATE TABLE `booking`
(
	booking_ref INT(11) NOT NULL AUTO_INCREMENT,
	arrival_date DATE DEFAULT NULL,
	departure_date DATE DEFAULT NULL,
	num_of_guests INT(11) DEFAULT NULL,
	cust_ref INT(11),
	PRIMARY KEY (`booking_ref`),
	FOREIGN KEY (`cust_ref`) REFERENCES customer(`cust_ref`)
);

CREATE TABLE `guest`
(
	pass_num NCHAR(10) NOT NULL,
	guest_lastname NCHAR(50) DEFAULT NULL,
	guest_firstname NCHAR(50) DEFAULT NULL,
	guest_DoB NCHAR(20) DEFAULT NULL,
    guest_diet_req NCHAR(50) DEFAULT NULL,
	booking_ref INT(11),
	PRIMARY KEY (`pass_num`),
	FOREIGN KEY (`booking_ref`) REFERENCES booking(`booking_ref`)
);

CREATE TABLE `breakfast`
(
	breakfast_ref INT(11) NOT NULL AUTO_INCREMENT,
	breakfast_price DECIMAL(10,2) DEFAULT 5.00,
	breakfast_num INT(11) DEFAULT NULL,
	booking_ref INT(11),
	PRIMARY KEY (`breakfast_ref`),
	FOREIGN KEY (`booking_ref`) REFERENCES booking(`booking_ref`)
);

CREATE TABLE `evening_meal`
(
	evening_meal_ref INT(11) NOT NULL AUTO_INCREMENT,
	evening_meal_price DECIMAL(10,2) DEFAULT 15.00,
	evening_meal_num INT(11) DEFAULT NULL,
	booking_ref INT(11),
	PRIMARY KEY (`evening_meal_ref`),
	FOREIGN KEY (`booking_ref`) REFERENCES booking(`booking_ref`)
);

CREATE TABLE `car_hire`
(
	car_hire_ref INT(11) NOT NULL AUTO_INCREMENT,
	car_hire_price DECIMAL(10,2) DEFAULT 50.00,
	car_hire_num INT(11) DEFAULT NULL,
	booking_ref INT(11),
	PRIMARY KEY (`car_hire_ref`),
	FOREIGN KEY (`booking_ref`) REFERENCES booking(`booking_ref`)
);
