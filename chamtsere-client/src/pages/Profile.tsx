import { useAuth0 } from '@auth0/auth0-react'
import { useNavigate } from 'react-router-dom'
import { useEffect, useState } from 'react'

export default function Profile() {
  const { isAuthenticated, isLoading, user } = useAuth0()
  const navigate = useNavigate()
  const [imgError, setImgError] = useState(false)

  // Redirect unauthenticated users back to dashboard
  useEffect(() => {
    if (!isLoading && !isAuthenticated) {
      navigate('/')
    }
  }, [isLoading, isAuthenticated, navigate])

  if (isLoading) return <div className="loading">Loading…</div>
  if (!isAuthenticated || !user) return null

  // Build a flat list of user claims from the Auth0 user object
  const claims = Object.entries(user).map(([type, value]) => ({
    type,
    value: typeof value === 'object' ? JSON.stringify(value) : String(value),
  }))

  return (
    <main className="page">
      <div className="profile-card" style={{ maxWidth: '100%', marginBottom: '2rem' }}>
        <div className="profile-avatar">
          {!imgError && user.picture ? (
            <img src={user.picture} alt="avatar" onError={() => setImgError(true)} />
          ) : (
            (user.name ?? user.email ?? 'U')[0].toUpperCase()
          )}
        </div>
        <div className="profile-name">{user.name}</div>
        {user.email && <div className="profile-email">{user.email}</div>}
      </div>

      <h1 className="page-title">User Claims</h1>
      <p className="page-sub">All claims returned from Auth0 for your account.</p>

      <table className="claims-table">
        <thead>
          <tr>
            <th>Claim Type</th>
            <th>Value</th>
          </tr>
        </thead>
        <tbody>
          {claims.map(c => (
            <tr key={c.type}>
              <td>{c.type}</td>
              <td>{c.value}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </main>
  )
}
