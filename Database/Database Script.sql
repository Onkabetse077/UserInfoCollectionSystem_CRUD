CREATE TABLE users(
id INT PRIMARY KEY IDENTITY(1,1),
full_name VARCHAR(MAX) NULL,
gender VARCHAR(MAX) NULL,
contact VARCHAR(MAX) NULL,
email VARCHAR(MAX) NULL,
birth_date DATE NULL,
date_inserted DATE NULL,
date_updated DATE NULL
);

SELECT * FROM users;
