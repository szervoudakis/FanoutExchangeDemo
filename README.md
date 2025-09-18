# 📨 FanoutExchangeDemo

An event-driven microservice architecture built with **.NET 8**, **RabbitMQ**, and **Entity Framework Core**, designed to handle user registration and trigger chained events through fanout exchanges.

---

## 🚀 Technologies Used

- C# / .NET 8  
- ASP.NET Core Web API  
- Entity Framework Core  
- RabbitMQ (Fanout Exchanges)  
- SQL Server  
- JWT Authentication  
- Password Hashing via Microsoft Identity  

---

## 🧱 Project Structure

The solution consists of five independent components:

### 1. **UserService (API)**
- Provides endpoints for user registration (`/register`) and login (`/login`)
- Implements JWT-based authentication
- On successful registration, publishes a message to the `user_events` RabbitMQ exchange

### 2. **ProducerApp**
- A standalone console app that sends test messages to the `user_events` exchange
- Useful for debugging and testing the RabbitMQ pipeline independently

### 3. **EmailConsumerApp**
- Listens to messages from the `user_events` exchange
- Simulates sending a welcome email to the user
- After sending the email, publishes a new message to the `payment_events` exchange

### 4. **PaymentConsumerApp**
- Subscribes to the `payment_events` exchange
- Simulates the creation of a payment record based on the received message

### 5. **LoggerConsumerApp**
- Also listens to the `payment_events` exchange
- Logs all received messages for monitoring and traceability

---

## 📁 How to Run

```plaintext
1) Ensure RAbbitMQ is running type in cmd docker-compose up -d
2) cd UserService
3) dotnet run
4) Run the consumers: EmailConsumerApp, PaymentConsumerApp, and LoggerConsumerApp
5) Send a POST request to /api/auth/register to trigger the event chain

