# PlayersWallet API 🚀

## Setup with Docker Compose

1. Clone the repository:  
   ```bash
   git clone https://github.com/vladyslav-pavliuk/players-wallet.git
   cd players-wallet
   ```  
2. Start the application and Redis using Docker Compose:  
   ```bash
   docker-compose up --build
   ```  
3. The API will be available at:  
   [http://localhost:8080](http://localhost:8080)  

## API Request Examples  

### Register a Player  
**Request:**  
```bash
POST http://localhost:8080/api/wallet/register
Content-Type: application/json

{
  "playerId": "123e4567-e89b-12d3-a456-426614174000"
}
```  

### Credit a Transaction  
**Request:**  
```bash
POST http://localhost:8080/api/wallet/transaction
Content-Type: application/json

{
  "playerId": "123e4567-e89b-12d3-a456-426614174000",
  "transactionId": "223e4567-e89b-12d3-a456-426614174000",
  "amount": 50.00,
  "type": "Deposit"
}
