# RenterScoreAPIv2

- RenterScoreAPIv2 is a .NET 8.0-based API designed to provide scoring and analytics for rental properties. It includes features like Swagger integration, unit testing, and CI/CD pipelines.

## Features
- Built with .NET 8.0
- Swagger for API documentation
- Unit testing with NUnit
- CI/CD pipelines using GitHub Actions
- Dockerized for containerized deployment
- Ansible playbooks for server provisioning and deployment

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/)
- [Ansible](https://www.ansible.com/)
- [Node.js](https://nodejs.org/) (for Snyk CLI)

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/Innovations-Technology/RenterScoreAPIv2.git
   cd RenterScoreAPIv2
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Build the project:
   ```bash
   dotnet build
   ```

4. Run the application:
   ```bash
   dotnet run --project RenterScoreAPIv2
   ```

5. Access the API:
   - Swagger UI: [http://localhost:5189/swagger](http://localhost:5189/swagger)

## Running Tests
To run unit tests:
```bash
dotnet test RenterScoreAPIv2.Test.Unit
```

## Docker Deployment
1. Build the Docker image:
   ```bash
   docker build -t renter-score-api-v2 .
   ```

2. Run the Docker container:
   ```bash
   docker run -p 5000:5000 renter-score-api-v2
   ```

## CI/CD Pipelines
This project includes GitHub Actions workflows for:
- Unit testing (`.github/workflows/unit-test.yml`)
- Code coverage (`.github/workflows/code-coverage.yml`)
- Security scanning with Snyk (`.github/workflows/snyk.yml`)
- Deployment to development (`.github/workflows/deploy-development.yml`)

## Ansible Deployment
To deploy using Ansible:
1. Ensure the inventory file is updated with your server details.
2. Run the playbook:
   ```bash
   ansible-playbook Ansible/deploy-development.yml -i Ansible/inventory
   ```

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request.

## License
This project is licensed under the MIT License.
