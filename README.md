Sistema de gestão dos utlizadores da Pastoral.
Será usado o serviço da Cloud do Azure

# Enum
### Status
| Variavel     | Value |
|--------------|-------|
| Pending      |   P   |
| Suspended    |   S   |
| Created      |   C   |
| Deleted      |   D   |


# Entities
### Security
| Type         | Variavel     | Descrition |
|--------------|--------------|------------|
| string       | Id           |            |
| string       | Name         |            |
| string       | Email        |            |
| string       | Phone        |            |
| string       | IdUser       |            |
| Status       | Status       |            |
| DateTime     | StartDate    |            |
| DateTime     | EndDate      |            |
| DateTime     | CreatedAt    |            |
| DateTime     | UpdatedAt    |            |
| DateTime     | DeletedAt    |            |
| bool         | IsDeleted    |            |

<br/>

# Linguagens, Ferramentas e Tecnologias
<div align="left">
  <p align="left">
    <a href="https://go-skill-icons.vercel.app/">
      <img src="https://go-skill-icons.vercel.app/api/icons?i=cs,dotnet,sqlserver,rabbitmq,git,docker,sonarqube,swagger,postman,githubactions" />
    </a>
  </p>
</div> <br/>

# Monitoramento
<div align="left">
  <p align="left">
    <a href="https://go-skill-icons.vercel.app/">
      <img src="https://go-skill-icons.vercel.app/api/icons?i=prometheus,grafana" />
    </a>
  </p>
</div> <br/>

# Observabilidade e Tracing
![Jaeger_OpenTelemetry](https://github.com/user-attachments/assets/bac7e17b-c42c-48a8-83ab-c0c3c1b0f3dc)

<br/>

# Migration
Add-Migration FirstMigration -o Persistence/Migrations
Update-database
