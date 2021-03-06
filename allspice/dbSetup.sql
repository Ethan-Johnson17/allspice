CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';
CREATE TABLE recipes(
  id int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
  create_time DATETIME COMMENT 'Create Time',
  update_time DATETIME COMMENT 'Update Time',
  title TEXT NOT NULL,
  subtitle TEXT NOT NULL,
  category TEXT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
) DEFAULT CHARSET UTF8 COMMENT '';
ALTER TABLE
  steps
ADD
  FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE;
CREATE TABLE ingredients(
    create_time DATETIME COMMENT 'Create Time',
    update_time DATETIME COMMENT 'Update Time',
    inName TEXT NOT NULL,
    quantity TEXT NOT NULL,
    recipeId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
  ) DEFAULT CHARSET UTF8 COMMENT '';
CREATE TABLE steps(
    id INT NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    create_time DATETIME COMMENT 'Create Time',
    stepOrder INT NOT NULL,
    bodyText TEXT NOT NULL,
    recipeId INT NOT NULL,
    creatorId VARCHAR(255) NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
    FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
  ) DEFAULT CHARSET UTF8 COMMENT '';
CREATE TABLE favorites(
    favoriteId int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    create_time DATETIME COMMENT 'Create Time',
    update_time DATETIME COMMENT 'Update Time',
    accountId VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
    FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
  ) DEFAULT CHARSET UTF8 COMMENT '';
CREATE TABLE tries(
    tryId int NOT NULL PRIMARY KEY AUTO_INCREMENT COMMENT 'Primary Key',
    create_time DATETIME COMMENT 'Create Time',
    update_time DATETIME COMMENT 'Update Time',
    accountId VARCHAR(255) NOT NULL,
    recipeId INT NOT NULL,
    FOREIGN KEY (recipeId) REFERENCES recipes(id) ON DELETE CASCADE,
    FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE
  ) DEFAULT CHARSET UTF8 COMMENT '';
DROP Table ingredients;