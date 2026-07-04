import { useAuth0 } from '@auth0/auth0-react'
import { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom'

interface UserListDto {
  id: string
  userName: string
  firstName: string
  lastName: string
  email: string
  phoneNumber: string
  roles: string[]
}

export default function Users() {
  const { isAuthenticated, isLoading, user, getAccessTokenSilently } = useAuth0()
  const navigate = useNavigate()
  const [users, setUsers] = useState<UserListDto[]>([])
  const [error, setError] = useState<string | null>(null)
  const [loadingUsers, setLoadingUsers] = useState(true)

  // Find roles in Auth0 claims
  const getRoles = (u: any): string[] => {
    if (!u) return []
    const rolesKey = Object.keys(u).find(key => key.endsWith('/roles')) || 'roles'
    const val = u[rolesKey]
    if (Array.isArray(val)) return val
    if (typeof val === 'string') return [val]
    return []
  }

  const isAdmin = getRoles(user).includes('Admin')

  useEffect(() => {
    if (!isLoading) {
      if (!isAuthenticated) {
        navigate('/')
        return
      }
      if (!isAdmin) {
        setError('Access Denied: You must be an Admin to view this page.')
        setLoadingUsers(false)
        return
      }
      fetchUsers()
    }
  }, [isLoading, isAuthenticated, isAdmin, navigate])

  const fetchUsers = async () => {
    try {
      setLoadingUsers(true)
      const token = await getAccessTokenSilently()
      const response = await fetch('/api/user/get-all', {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      })

      if (response.status === 403) {
        setError('Forbidden (403): You do not have permission to view users. Please make sure the Admin role is correctly mapped to your token in your Auth0 dashboard.')
        return
      }

      if (!response.ok) {
        throw new Error(`Failed to fetch: ${response.statusText}`)
      }

      const data = await response.json()
      setUsers(data)
    } catch (err: any) {
      setError(err.message || 'An unexpected error occurred while loading users.')
    } finally {
      setLoadingUsers(false)
    }
  }

  if (isLoading || loadingUsers) {
    return <div className="loading">Loading users…</div>
  }

  return (
    <main className="page">
      <h1 className="page-title">User Management</h1>
      <p className="page-sub">Admin view containing all registered platform users.</p>

      {error ? (
        <div style={{
          background: 'rgba(248, 113, 113, 0.1)',
          border: '1px solid rgba(248, 113, 113, 0.25)',
          borderRadius: 'var(--radius)',
          padding: '1.5rem',
          color: 'var(--danger)',
          fontSize: '0.9rem',
          lineHeight: '1.6',
          marginBottom: '2rem'
        }}>
          <strong>{error}</strong>
          {!isAdmin && (
            <p style={{ marginTop: '0.75rem', color: 'var(--muted)' }}>
              If you already assigned the Admin role in Auth0, make sure you created an Auth0 Action or Rule to add roles to your access token custom claims.
            </p>
          )}
        </div>
      ) : (
        <div style={{ overflowX: 'auto' }}>
          <table className="claims-table" style={{ width: '100%' }}>
            <thead>
              <tr>
                <th>ID</th>
                <th>Username</th>
                <th>Full Name</th>
                <th>Email</th>
                <th>Phone</th>
                <th>Roles</th>
              </tr>
            </thead>
            <tbody>
              {users.length === 0 ? (
                <tr>
                  <td colSpan={6} style={{ textAlign: 'center', color: 'var(--muted)', padding: '2rem' }}>
                    No users found.
                  </td>
                </tr>
              ) : (
                users.map(u => (
                  <tr key={u.id}>
                    <td>{u.id}</td>
                    <td>{u.userName}</td>
                    <td>{u.firstName || '-'} {u.lastName || '-'}</td>
                    <td>{u.email || '-'}</td>
                    <td>{u.phoneNumber || '-'}</td>
                    <td>
                      <div style={{ display: 'flex', gap: '0.4rem', flexWrap: 'wrap' }}>
                        {u.roles && u.roles.length > 0 ? (
                          u.roles.map(r => (
                            <span key={r} style={{
                              fontSize: '0.75rem',
                              padding: '0.2rem 0.6rem',
                              borderRadius: '99px',
                              background: r === 'Admin' ? 'rgba(192, 132, 252, 0.15)' : 'rgba(124, 107, 255, 0.12)',
                              color: r === 'Admin' ? 'var(--accent2)' : 'var(--accent)',
                              border: `1px solid ${r === 'Admin' ? 'rgba(192, 132, 252, 0.25)' : 'rgba(124, 107, 255, 0.25)'}`,
                              fontWeight: 600
                            }}>
                              {r}
                            </span>
                          ))
                        ) : (
                          <span style={{ color: 'var(--muted)', fontSize: '0.8rem' }}>None</span>
                        )}
                      </div>
                    </td>
                  </tr>
                ))
              )}
            </tbody>
          </table>
        </div>
      )}
    </main>
  )
}
