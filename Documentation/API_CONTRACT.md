# Mafqoodi API Contract

| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| Register | POST | `/api/auth/register` | No | Any |
| Login | POST | `/api/auth/login` | No | Any |
| Reports | GET | `/api/reports` | No | Any |
| Report details | GET | `/api/reports/{id}` | No | Any |
| My reports | GET | `/api/reports/mine` | JWT | User/Admin |
| Create report | POST | `/api/reports` | JWT | User/Admin |
| Update report | PUT | `/api/reports/{id}` | JWT | Owner |
| Delete report | DELETE | `/api/reports/{id}` | JWT | Owner/Admin |
| Dashboard statistics | GET | `/api/admin/statistics` | JWT | Admin |
| Users | GET | `/api/admin/users` | JWT | Admin |
| Ban/unban user | PATCH | `/api/admin/users/{id}/status` | JWT | Admin |
| Assign admin | POST | `/api/admin/users/{id}/assign-admin` | JWT | Admin |
| Report admin status | PATCH | `/api/admin/reports/{id}/status` | JWT | Admin |
| Health | GET | `/health` | No | Any |

All JSON errors use RFC 7807-style `ProblemDetails` responses. Flutter must communicate through these endpoints rather than MongoDB/Firestore.
