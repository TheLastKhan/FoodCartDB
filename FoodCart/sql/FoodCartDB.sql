/*
CREATE TABLE Users (
    user_id SERIAL PRIMARY KEY,
    fullname VARCHAR(255) NOT NULL,
    user_email VARCHAR(255) NOT NULL UNIQUE,
    user_phone_number VARCHAR(20),
    password VARCHAR(255) NOT NULL,
    user_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Restaurants (
    restaurant_id SERIAL PRIMARY KEY,
    restaurant_name VARCHAR(255) NOT NULL,
    restaurant_description TEXT,
    location VARCHAR(255),
    restaurant_rating NUMERIC(3,1),
    opening_hours VARCHAR(255),
    deal_title VARCHAR(255),
    deal_description TEXT,
    phone VARCHAR(20),
    restaurant_email VARCHAR(255)
);

CREATE TABLE Meals (
    meal_id SERIAL PRIMARY KEY,
    restaurant_id INTEGER NOT NULL REFERENCES Restaurants(restaurant_id),
    meal_name VARCHAR(255) NOT NULL,
    meal_description TEXT,
    meal_price NUMERIC(10,2) NOT NULL,
    category VARCHAR(50)
);

CREATE TABLE Orders (
    order_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES Users(user_id),
    restaurant_id INTEGER NOT NULL REFERENCES Restaurants(restaurant_id),
    order_date TIMESTAMP NOT NULL,
    total_amount NUMERIC(10,2) NOT NULL,
    delivery_type VARCHAR(50),
    payment_method VARCHAR(50),
    status VARCHAR(50)
);

CREATE TABLE OrderDetails (
    order_detail_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NULL REFERENCES Orders(order_id),
    meal_id INTEGER NOT NULL REFERENCES Meals(meal_id),
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    meal_price NUMERIC(10,2) NOT NULL
);

CREATE TABLE Coupons (
    coupon_id SERIAL PRIMARY KEY,
    code VARCHAR(50) UNIQUE,
    discount_percentage NUMERIC(5,2) CHECK (discount_percentage BETWEEN 0 AND 100),
    expiry_date DATE NOT NULL,
    user_id INTEGER REFERENCES Users(user_id),
    used_in_order_id INTEGER REFERENCES Orders(order_id)
);

CREATE TABLE Deals (
    deal_id SERIAL PRIMARY KEY,
    restaurant_id INTEGER NOT NULL REFERENCES Restaurants(restaurant_id),
    deal_description TEXT,
    start_date DATE NOT NULL,
    end_date DATE NOT NULL
);

CREATE TABLE Reviews (
    review_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES Users(user_id),
    restaurant_id INTEGER NOT NULL REFERENCES Restaurants(restaurant_id),
    order_id INTEGER REFERENCES Orders(order_id),
    review_rating NUMERIC(2,1) CHECK (review_rating BETWEEN 1 AND 5),
    review_comment TEXT,
    review_created_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Favorites (
    favorite_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES Users(user_id),
    restaurant_id INTEGER NOT NULL REFERENCES Restaurants(restaurant_id),
    menu_id INTEGER NOT NULL REFERENCES Meals(meal_id),
    favorite_added_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(user_id, restaurant_id, menu_id)
);

CREATE TABLE Cart (
    cart_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES Users(user_id),
    meal_id INTEGER NOT NULL REFERENCES Meals(meal_id),
    quantity INTEGER NOT NULL CHECK (quantity > 0),
    cart_added_at TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE PaymentInfo (
    payment_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES Users(user_id),
    card_number VARCHAR(16) NOT NULL,
    card_holder_name VARCHAR(255) NOT NULL,
    expiry_date DATE NOT NULL,
    cvv_code VARCHAR(3) NOT NULL
);

CREATE TABLE DeliveryServices (
    delivery_service_id SERIAL PRIMARY KEY,
    order_id INTEGER NOT NULL REFERENCES Orders(order_id),
    delivery_service_name VARCHAR(255),
    delivery_fee NUMERIC(10,2),
    estimated_delivery_time TEXT
);

CREATE TABLE Address (
    address_id SERIAL PRIMARY KEY,
    user_id INTEGER NOT NULL REFERENCES Users(user_id),
    street_address VARCHAR(255) NOT NULL,
    city VARCHAR(100) NOT NULL,
    state VARCHAR(100),
    postal_code VARCHAR(20),
    country VARCHAR(100)
);
*/

/*
INSERT INTO Users (fullname,user_email, user_phone_number, password, user_created_at)
VALUES 
('John Doe', 'john.doe@example.com', '5551234567', 'password123', '2024-12-21 08:00:00'),
('Jane Smith', 'jane.smith@example.com', '5559876543', 'securepass456', '2024-12-21 09:00:00'),
('Alice Brown', 'alice.brown@example.com', '5555551212', 'mypassword789', '2024-12-20 17:30:00'),
('Bob Johnson', 'bob.johnson@example.com', '5553334444', 'password111', '2024-12-19 12:00:00'),
('Charlie Davis', 'charlie.davis@example.com', '5551112222', 'ilovecoding', '2024-12-18 10:00:00'),
('Emily Clark', 'emily.clark@example.com', '5554443333', 'pass1234', '2024-12-20 11:45:00'),
('Daniel Lee', 'daniel.lee@example.com', '5558887777', 'testpass', '2024-12-19 15:00:00'),
('Grace Hill', 'grace.hill@example.com', '5559990000', 'gracefulpass', '2024-12-18 13:30:00'),
('Henry Scott', 'henry.scott@example.com', '5556665555', 'mypassword222', '2024-12-21 14:20:00'),
('Sophia Taylor', 'sophia.taylor@example.com', '5554448888', 'secureme999', '2024-12-19 16:45:00');
*/

/*
INSERT INTO Restaurants (restaurant_name, restaurant_description, location, restaurant_rating, opening_hours, deal_title, deal_description, phone, restaurant_email)
VALUES 
('Pizza Palace', 'Delicious handmade pizzas', 'Downtown', 4.5, '10:00-22:00', 'Free Drink', 'Get a free drink with every pizza', '5551114444', 'info@pizzapalace.com'),
('Burger Barn', 'Best burgers in town', 'Main Street', 4.3, '11:00-23:00', 'Burger Deal', 'Buy 1, get 1 half off', '5552225555', 'contact@burgerbarn.com'),
('Sushi Spot', 'Fresh sushi and sashimi', 'Market Square', 4.7, '12:00-21:00', 'Sushi Special', '20% off on Fridays', '5553336666', 'hello@sushispot.com'),
('Taco Town', 'Authentic Mexican tacos', 'Central Avenue', 4.6, '11:00-20:00', 'Taco Tuesday', 'Tacos for $1 on Tuesdays', '5554447777', 'info@tacotown.com'),
('Pasta Paradise', 'Italian pasta dishes', 'River Road', 4.4, '12:00-22:00', 'Pasta Party', 'Kids eat free on weekends', '5555558888', 'contact@pastaparadise.com'),
('BBQ Joint', 'Smoked barbecue', 'Hilltop Road', 4.8, '12:00-21:30', 'BBQ Feast', '15% off orders over $50', '5556669999', 'bbq@joint.com'),
('Veggie Delight', 'Vegan and vegetarian meals', 'Green Street', 4.6, '10:00-20:00', 'Healthy Deal', 'Free smoothie with salad', '5557770000', 'hello@veggiedelight.com'),
('Seafood Shack', 'Fresh seafood dishes', 'Coastal Road', 4.5, '11:00-21:00', 'Seafood Sensation', '10% off all orders', '5558881111', 'info@seafoodshack.com'),
('Steakhouse Supreme', 'Premium steaks and sides', 'Main Boulevard', 4.9, '17:00-23:00', 'Steak Night', 'Free dessert with steak', '5559992222', 'contact@steakhouse.com'),
('Dessert Dreams', 'Cakes and pastries', 'Sweet Lane', 4.8, '08:00-20:00', 'Sweet Deal', 'Buy 1, get 1 free', '5550003333', 'hello@dessertdreams.com');
*/

/*
INSERT INTO Meals (restaurant_id, meal_name, meal_description, meal_price, category)
VALUES 
(1, 'Pepperoni Pizza', 'Classic pepperoni with cheese', 12.99, 'Main Course'),
(1, 'Margherita Pizza', 'Fresh tomato and basil', 10.99, 'Main Course'),
(2, 'Cheeseburger', 'Beef patty with cheese', 9.99, 'Main Course'),
(2, 'Veggie Burger', 'Grilled veggies and avocado', 8.99, 'Main Course'),
(3, 'California Roll', 'Crab and avocado sushi', 7.99, 'Starter'),
(3, 'Tuna Sashimi', 'Slices of fresh tuna', 11.99, 'Starter'),
(4, 'Chicken Tacos', 'Spicy chicken in a taco', 3.99, 'Main Course'),
(4, 'Beef Tacos', 'Shredded beef with salsa', 4.49, 'Main Course'),
(5, 'Spaghetti Carbonara', 'Creamy pasta with bacon', 13.99, 'Main Course'),
(5, 'Penne Alfredo', 'Pasta with creamy alfredo sauce', 12.49, 'Main Course');
*/

/*
INSERT INTO Orders (user_id, restaurant_id, order_date, total_amount, delivery_type, payment_method, status)
VALUES 
(1, 1, '2024-12-21 12:30:00', 25.98, 'Delivery', 'Card', 'Completed'),
(2, 2, '2024-12-21 13:00:00', 18.98, 'Pickup', 'Cash', 'Completed'),
(3, 3, '2024-12-20 18:00:00', 19.98, 'Delivery', 'Online Payment', 'Pending'),
(4, 4, '2024-12-20 19:30:00', 8.48, 'Pickup', 'Card', 'Completed'),
(5, 5, '2024-12-19 20:00:00', 26.48, 'Delivery', 'Cash', 'Cancelled'),
(6, 6, '2024-12-19 18:30:00', 15.99, 'Delivery', 'Card', 'Completed'),
(7, 7, '2024-12-18 12:00:00', 9.99, 'Pickup', 'Online Payment', 'Completed'),
(8, 8, '2024-12-18 13:15:00', 16.99, 'Delivery', 'Card', 'Completed'),
(9, 9, '2024-12-21 14:45:00', 22.99, 'Delivery', 'Cash', 'Completed'),
(10, 10, '2024-12-19 15:00:00', 19.98, 'Pickup', 'Card', 'Completed');
*/

/*
INSERT INTO OrderDetails (order_id, meal_id, quantity, meal_price)
VALUES 
(1, 1, 2, 12.99),
(1, 2, 1, 10.99),
(2, 3, 1, 9.99),
(2, 4, 1, 8.99),
(3, 5, 2, 7.99),
(3, 6, 1, 11.99),
(4, 7, 2, 3.99),
(4, 8, 1, 4.49),
(5, 9, 2, 13.99),
(5, 10, 1, 12.49);
*/

/*
INSERT INTO Coupons (code, discount_percentage, expiry_date, user_id, used_in_order_id)
VALUES 
('DISCOUNT10', 10.00, '2025-01-01', 1, NULL),
('SAVE15', 15.00, '2025-01-15', 2, 2),
('FREEMEAL', 100.00, '2024-12-31', 3, NULL),
('SUMMER20', 20.00, '2024-12-30', 4, 4),
('WELCOME5', 5.00, '2025-02-01', 5, NULL),
('HAPPYHOUR', 25.00, '2024-12-25', 6, NULL),
('WINTER30', 30.00, '2025-01-31', 7, NULL),
('LOYALTY50', 50.00, '2025-03-01', 8, NULL),
('EXTRA10', 10.00, '2025-01-10', 9, NULL),
('FLASHDEAL', 40.00, '2024-12-22', 10, 10);
*/

/*
INSERT INTO Deals (restaurant_id, deal_description, start_date, end_date)
VALUES 
(1, 'Buy 2 pizzas, get 1 free!', '2024-12-20', '2025-01-01'),
(2, 'Free fries with any burger.', '2024-12-21', '2025-01-15'),
(3, 'Sushi platter 20% off.', '2024-12-22', '2025-01-10'),
(4, 'Tacos for $1 on Tuesdays.', '2024-12-20', '2025-01-30'),
(5, 'Free garlic bread with pasta.', '2024-12-20', '2025-02-01'),
(6, 'Family BBQ meal discount.', '2024-12-25', '2025-01-31'),
(7, 'Free smoothie with every meal.', '2024-12-22', '2025-02-15'),
(8, '10% off seafood platters.', '2024-12-20', '2025-01-31'),
(9, 'Free dessert with steak.', '2024-12-24', '2025-01-15'),
(10, 'Buy 1 pastry, get 1 free.', '2024-12-20', '2025-01-05');
*/

/*
INSERT INTO Reviews (user_id, restaurant_id, order_id, review_rating, review_comment, review_created_at)
VALUES 
(1, 1, 1, 4.5, 'Great pizza, loved the crust!', '2024-12-21 13:00:00'),
(2, 2, 2, 4.0, 'Tasty burger, fries could be better.', '2024-12-21 14:00:00'),
(3, 3, 3, 4.8, 'Fresh and delicious sushi.', '2024-12-20 19:00:00'),
(4, 4, 4, 4.2, 'Tacos were good but a bit spicy.', '2024-12-20 20:00:00'),
(5, 5, 5, 3.5, 'Pasta was okay, nothing special.', '2024-12-19 21:00:00'),
(6, 6, 6, 5.0, 'Best BBQ I have ever had!', '2024-12-19 19:00:00'),
(7, 7, NULL, 4.7, 'Healthy and delicious meals.', '2024-12-18 13:00:00'),
(8, 8, NULL, 4.3, 'Good seafood, slightly overpriced.', '2024-12-18 14:00:00'),
(9, 9, 9, 4.9, 'Steak was cooked perfectly.', '2024-12-21 15:00:00'),
(10, 10, 10, 5.0, 'Desserts were heavenly!', '2024-12-19 16:00:00');
*/

/*
INSERT INTO Favorites (user_id, restaurant_id, menu_id, favorite_added_at)
VALUES 
(1, 1, 1, '2024-12-21 08:00:00'),
(2, 2, 3, '2024-12-21 09:00:00'),
(3, 3, 5, '2024-12-20 17:30:00'),
(4, 4, 7, '2024-12-19 12:00:00'),
(5, 5, 9, '2024-12-18 10:00:00'),
(6, 6, 10, '2024-12-20 11:45:00'),
(7, 7, 2, '2024-12-19 15:00:00'),
(8, 8, 4, '2024-12-18 13:30:00'),
(9, 9, 6, '2024-12-21 14:20:00'),
(10, 10, 8, '2024-12-19 16:45:00');
*/

/*
INSERT INTO Cart (user_id, meal_id, quantity, cart_added_at)
VALUES 
(1, 1, 2, '2024-12-21 10:00:00'),
(2, 2, 1, '2024-12-21 11:00:00'),
(3, 3, 3, '2024-12-20 12:30:00'),
(4, 4, 2, '2024-12-20 13:15:00'),
(5, 5, 1, '2024-12-20 14:45:00'),
(6, 6, 4, '2024-12-19 09:00:00'),
(7, 7, 2, '2024-12-19 10:30:00'),
(8, 8, 1, '2024-12-19 11:45:00'),
(9, 9, 3, '2024-12-19 12:20:00'),
(10, 10, 1, '2024-12-18 15:30:00');
*/

/*
INSERT INTO PaymentInfo (user_id, card_number, card_holder_name, expiry_date, cvv_code)
VALUES 
(1, '4111111111111111', 'John Smith', '2026-05-01', '123'),
(2, '4222222222222222', 'Alice Johnson', '2025-10-01', '456'),
(3, '4333333333333333', 'Robert Brown', '2027-08-01', '789'),
(4, '4444444444444444', 'Emily Davis', '2024-12-01', '012'),
(5, '4555555555555555', 'Michael Wilson', '2026-07-01', '345'),
(6, '4666666666666666', 'Sophia Garcia', '2025-03-01', '678'),
(7, '4777777777777777', 'James Martinez', '2026-11-01', '901'),
(8, '4888888888888888', 'Isabella Anderson', '2027-04-01', '234'),
(9, '4999999999999999', 'Olivia Taylor', '2025-06-01', '567'),
(10, '4000000000000000', 'William Harris', '2026-09-01', '890');
*/

/*
INSERT INTO DeliveryServices (order_id, delivery_service_name, delivery_fee, estimated_delivery_time)
VALUES 
(1, 'FastExpress', 10.00, '30-40 mins'),
(2, 'QuickDelivery', 8.50, '20-30 mins'),
(3, 'FoodDash', 12.00, '25-35 mins'),
(4, 'SafeRide', 7.00, '40-50 mins'),
(5, 'SpeedyEats', 9.99, '15-20 mins'),
(6, 'ExpressGrub', 11.50, '30-40 mins'),
(7, 'RapidBites', 8.00, '25-35 mins'),
(8, 'SwiftMeals', 10.50, '20-30 mins'),
(9, 'InstantFood', 7.99, '35-45 mins'),
(10, 'TastyRide', 9.00, '30-40 mins');
*/

/*
INSERT INTO Address (user_id, street_address, city, state, postal_code, country)
VALUES 
(1, '123 Main Street', 'New York', 'NY', '10001', 'USA'),
(2, '456 Elm Road', 'Los Angeles', 'CA', '90001', 'USA'),
(3, '789 Oak Lane', 'Chicago', 'IL', '60601', 'USA'),
(4, '101 Pine Street', 'Houston', 'TX', '77001', 'USA'),
(5, '202 Maple Ave', 'Phoenix', 'AZ', '85001', 'USA'),
(6, '303 Cedar Court', 'Philadelphia', 'PA', '19101', 'USA'),
(7, '404 Birch Way', 'San Antonio', 'TX', '78201', 'USA'),
(8, '505 Walnut Blvd', 'San Diego', 'CA', '92101', 'USA'),
(9, '606 Chestnut Drive', 'Dallas', 'TX', '75201', 'USA'),
(10, '707 Aspen Rd', 'San Jose', 'CA', '95101', 'USA');
*/

