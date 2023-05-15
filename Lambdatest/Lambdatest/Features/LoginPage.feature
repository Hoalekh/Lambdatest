Feature: LoginPage

A short summary of the feature

Feature: AnimatedGiftCardsCartTotalAmountValidation
Simple test cases for validation of total amount value
Fill invalid email >>> verify warning message under field
Fill valid email + Incorrect password >>> click login

Fill valid email and password >>> click login >>> verify content



Scenario: Test Login Lambdatest with invalid email
		Given I navigate to login page "https://accounts.lambdatest.com/login"
		Then I verify page login
		And I enter following login details
		| Email						| Password |
		| tranxuanviet91@gmailcom   | 123!@#qwE |
		Then I verify message "Invalid email address"

Scenario: Test Login Lambdatest with valid email and incorrect password
		Given I navigate to login page "https://accounts.lambdatest.com/login"
		Then I verify page login
		And I enter following login details
		| Email						| Password |
		| tranxuanviet91@gmail.com   | 123!@# |
		Then I Click to login 
		And I verify message password error

Scenario: Test Login Lambdatest with valid email and password
		Given I navigate to login page "https://accounts.lambdatest.com/login"
		Then I verify page login
		And I enter following login details
		| Email						| Password |
		| tranxuanviet91@gmail.com   | 123!@#qwE |
		Then I Click to login 
		And I verify login successfull