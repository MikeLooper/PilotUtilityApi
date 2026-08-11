---
description:architecture standards for work in this solution.
applyTo: "**.cs"
---

# Architecture Instructions

## Architecture and structural requirements:

- Keep controller layer thin: only HTTP concerns (status codes, request mapping, validation triggering).
- Implement business logic in the service layer.
- Isolate persistence in the repository layer.
- Keep mapping between domain models and DTOs in dedicated mappers.
- Use constructor injection everywhere.
- Follow SOLID principles and avoid God classes.
- Generate endpoint handlers that match operationIds and paths from the OpenAPI spec.
- Implement request/response DTOs exactly according to the spec schemas.
- Apply field and payload validation consistent with OpenAPI constraints.
- Implement consistent error responses that match spec-defined error models.
- Ensure content types, response codes, and required headers are respected.

## Cross-cutting and quality requirements:

- Global exception handling with a standardized error envelope.
- Input validation with clear, client-friendly error messages.
- Logging strategy:
  - structured logs
  - no sensitive data in logs
  - meaningful contextual fields
- API documentation exposure aligned with OpenAPI source.
- Use pagination/sorting/filtering patterns where endpoints indicate collection access.

## Data and persistence requirements (if persistence is required):

- Use Dapper persistence technology.
- Use clear entity modeling and repository abstractions.
- Keep transactional boundaries in service layer.

## Non-functional requirements:

- Clear README with setup, run, test, and environment configuration steps.
- Deterministic build and runnable local profile.
- Idiomatic naming conventions and consistent formatting.
- No dead code, placeholders, or TODO stubs in core paths.


## Constraints

- Construction and behavior of the API endpoints should follow REST API best practices.
- Favor maintainability, testability, clear separation of concerns, and clean architecture principles.

## Guardrails:

- Do not skip layers for convenience.
- Do not place business logic in controllers.
- Do not couple persistence entities directly to external API contracts unless explicitly justified.
- Do not output pseudo-code where concrete implementation is expected.
