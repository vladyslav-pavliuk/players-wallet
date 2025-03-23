# PlayersWallet API 🚀

A .NET Core backend service for managing player wallets and transactions. It supports player registration, balance checks, transaction processing, and transaction history retrieval.

---

## Setup with Docker Compose

1. Clone the repository:
   ```bash
   git clone https://github.com/vladyslav-pavliuk/players-wallet.git
   cd players-wallet
2. Start the application and Redis using Docker Compose:
   ```bash
   docker-compose up --build
3. The API will be available at:
   ```bash
   http://localhost:8080

** API Request Examples **
  Register a Player
  Request:
  ```bash
  POST http://localhost:8080/api/wallet/register
  Content-Type: application/json
  
  {
    "playerId": "123e4567-e89b-12d3-a456-426614174000"
  }
