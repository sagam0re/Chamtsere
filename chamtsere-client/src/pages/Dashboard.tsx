import { useAuth0 } from '@auth0/auth0-react'
import { Link } from 'react-router-dom'
import { useState } from 'react'

const IconUser = () => (
  <svg width="17" height="17" viewBox="0 0 24 24" fill="currentColor">
    <path d="M12 12c2.7 0 4.8-2.1 4.8-4.8S14.7 2.4 12 2.4 7.2 4.5 7.2 7.2 9.3 12 12 12zm0 2.4c-3.2 0-9.6 1.6-9.6 4.8v2.4h19.2v-2.4c0-3.2-6.4-4.8-9.6-4.8z"/>
  </svg>
)

const IconLogin = () => (
  <svg width="17" height="17" viewBox="0 0 24 24" fill="currentColor">
    <path d="M11 7L9.6 8.4l2.6 2.6H2v2h10.2l-2.6 2.6L11 17l5-5-5-5zm9 12h-8v2h8c1.1 0 2-.9 2-2V5c0-1.1-.9-2-2-2h-8v2h8v14z"/>
  </svg>
)

const IconLogout = () => (
  <svg width="17" height="17" viewBox="0 0 24 24" fill="currentColor">
    <path d="M17 7l-1.41 1.41L18.17 11H8v2h10.17l-2.58 2.58L17 17l5-5-5-5zM4 5h8V3H4c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h8v-2H4V5z"/>
  </svg>
)

const stats = [
  { value: '2.4k', label: 'Active Clients' },
  { value: '98%',  label: 'Satisfaction Rate' },
  { value: '340',  label: 'Bookings This Month' },
]

export default function Dashboard() {
  const { isAuthenticated, isLoading, user, loginWithRedirect, logout } = useAuth0()
  const [imgError, setImgError] = useState(false)

  const getRoles = (u: any): string[] => {
    if (!u) return []
    const rolesKey = Object.keys(u).find(key => key.endsWith('/roles')) || 'roles'
    const val = u[rolesKey]
    if (Array.isArray(val)) return val
    if (typeof val === 'string') return [val]
    return []
  }

  const isAdmin = getRoles(user).includes('Admin')

  if (isLoading) {
    return <div className="loading">Loading…</div>
  }

  return (
    <section className="hero">
      <div className="blob blob-1" />
      <div className="blob blob-2" />

      {isAuthenticated ? (
        <>
          <div className="badge">
            <span className="badge-dot" />
            Welcome back
          </div>

          {/* Profile card */}
          <div className="profile-card">
            <div className="profile-avatar">
              {!imgError && user?.picture ? (
                <img src={user.picture} alt="avatar" onError={() => setImgError(true)} />
              ) : (
                (user?.name ?? user?.email ?? 'U')[0].toUpperCase()
              )}
            </div>
            <div className="profile-name">{user?.name}</div>
            {user?.email && <div className="profile-email">{user.email}</div>}
            <div className="profile-actions" style={{ flexDirection: 'column', width: '100%', gap: '0.75rem' }}>
              <div style={{ display: 'flex', gap: '0.75rem', width: '100%' }}>
                <Link id="dash-profile" to="/profile" className="btn btn-ghost" style={{ flex: 1, justifyContent: 'center' }}>
                  <IconUser />
                  View Profile
                </Link>
                <button
                  id="dash-logout"
                  className="btn btn-danger"
                  style={{ flex: 1, justifyContent: 'center' }}
                  onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
                >
                  <IconLogout />
                  Sign Out
                </button>
              </div>
              {isAuthenticated && (
                <Link id="dash-users" to="/users" className="btn btn-primary" style={{ width: '100%', justifyContent: 'center' }}>
                  <svg width="17" height="17" viewBox="0 0 24 24" fill="currentColor">
                    <path d="M16 11c1.66 0 2.99-1.34 2.99-3S17.66 5 16 5c-1.66 0-3 1.34-3 3s1.34 3 3 3zm-8 0c1.66 0 2.99-1.34 2.99-3S9.66 5 8 5C6.34 5 5 6.34 5 8s1.34 3 3 3zm0 2c-2.33 0-7 1.17-7 3.5V19h14v-2.5c0-2.33-4.67-3.5-7-3.5zm8 0c-.29 0-.62.02-.97.05 1.16.84 1.97 1.97 1.97 3.45V19h6v-2.5c0-2.33-4.67-3.5-7-3.5z"/>
                  </svg>
                  Manage Users {!isAdmin && <span style={{ fontSize: '0.65rem', marginLeft: '0.4rem', background: 'rgba(255,255,255,0.15)', color: '#fff', padding: '0.1rem 0.4rem', borderRadius: '4px' }}>Admin</span>}
                </Link>
              )}
            </div>
          </div>

          <h1 className="hero-title">
            Your <span className="grad">Dashboard</span>
          </h1>
          <p className="hero-sub">
            Everything you need to manage your salon — bookings, staff, and analytics — all in one place.
          </p>
        </>
      ) : (
        <>
          <div className="badge">
            <span className="badge-dot" />
            Salon Management Platform
          </div>
          <h1 className="hero-title">
            Run your salon<br /><span className="grad">effortlessly.</span>
          </h1>
          <p className="hero-sub">
            Manage bookings, staff, and clients from one beautiful dashboard. Sign in to get started.
          </p>
          <div className="cta-row">
            <button
              id="dash-login"
              className="btn btn-primary btn-lg"
              onClick={() => loginWithRedirect()}
            >
              <IconLogin />
              Sign In with Auth0
            </button>
          </div>
        </>
      )}

      <div className="stats">
        {stats.map(s => (
          <div key={s.label} className="stat-card">
            <div className="stat-value">{s.value}</div>
            <div className="stat-label">{s.label}</div>
          </div>
        ))}
      </div>
    </section>
  )
}
