# Mafqoodi API Contract

## Authentication
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| Register | POST | `/api/auth/register` | No | Any |
| Login | POST | `/api/auth/login` | No | Any |
| Request OTP | POST | `/api/auth/request-otp` | JWT | User/Admin |
| Verify OTP | POST | `/api/auth/verify-otp` | JWT | User/Admin |

## Reports
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| List reports | GET | `/api/reports` | No | Any |
| Report details | GET | `/api/reports/{id}` | No | Any |
| My reports | GET | `/api/reports/mine` | JWT | User/Admin |
| Create report | POST | `/api/reports` | JWT | User/Admin |
| Update report | PUT | `/api/reports/{id}` | JWT | Owner |
| Delete report | DELETE | `/api/reports/{id}` | JWT | Owner/Admin |

## Administration
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| Dashboard statistics | GET | `/api/admin/statistics` | JWT | Admin |
| Users | GET | `/api/admin/users` | JWT | Admin |
| Ban/unban user | PATCH | `/api/admin/users/{id}/status` | JWT | Admin |
| Assign admin | POST | `/api/admin/users/{id}/assign-admin` | JWT | Admin |
| Report admin status | PATCH | `/api/admin/reports/{id}/status` | JWT | Admin |
| Broadcast notification | POST | `/api/admin/notifications/broadcast` | JWT | Admin |

## Organizations
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| List organizations | GET | `/api/organizations` | No | Any |

## Notifications
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| User notifications | GET | `/api/notifications` | JWT | User/Admin |
| Mark as read | PATCH | `/api/notifications/{id}/read` | JWT | Owner |

## Support
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| My chats | GET | `/api/support/chats` | JWT | User/Admin |
| Messages | GET | `/api/support/chats/{chatId}/messages` | JWT | Chat owner |
| Send message | POST | `/api/support/chats/{chatId}/messages` | JWT | Chat owner |

## Smart Matching
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| Find matches | GET | `/api/smart-matching/{reportId}` | JWT | User/Admin |

## System
| Feature | Method | Endpoint | Auth | Role |
|---|---|---|---|---|
| Health | GET | `/health` | No | Any |

All JSON errors use RFC 7807-style `ProblemDetails`. Flutter communicates through this HTTP boundary; direct MongoDB/Firestore access is not part of the rebuilt application contract.
