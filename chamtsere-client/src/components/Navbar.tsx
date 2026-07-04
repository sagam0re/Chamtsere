import { useAuth0 } from '@auth0/auth0-react'
import { Link } from 'react-router-dom'
import { useState } from 'react'


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

export default function Navbar() {
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

  return (
    <nav className="topbar">
      <Link to="/" className="nav-brand">
        <span className="nav-brand-dot" />
        Chamtsere
      </Link>

      <div className="nav-actions">
        {isLoading ? (
          <span style={{ color: 'var(--muted)', fontSize: '0.85rem' }}>Loading…</span>
        ) : isAuthenticated ? (
          <>
            {isAuthenticated && (
              <Link id="nav-users" to="/users" className="btn btn-ghost" style={{ display: 'flex', alignItems: 'center', gap: '0.45rem' }}>
                <svg width="16" height="16" viewBox="0 0 24 24" fill="currentColor">
                  <path d="M16 11c1.66 0 2.99-1.34 2.99-3S17.66 5 16 5c-1.66 0-3 1.34-3 3s1.34 3 3 3zm-8 0c1.66 0 2.99-1.34 2.99-3S9.66 5 8 5C6.34 5 5 6.34 5 8s1.34 3 3 3zm0 2c-2.33 0-7 1.17-7 3.5V19h14v-2.5c0-2.33-4.67-3.5-7-3.5zm8 0c-.29 0-.62.02-.97.05 1.16.84 1.97 1.97 1.97 3.45V19h6v-2.5c0-2.33-4.67-3.5-7-3.5z"/>
                </svg>
                Users {isAdmin && <span style={{ fontSize: '0.65rem', background: 'rgba(192, 132, 252, 0.2)', color: 'var(--accent2)', padding: '0.1rem 0.35rem', borderRadius: '4px', fontWeight: 600 }}>Admin</span>}
              </Link>
            )}
            <Link id="nav-profile" to="/profile" className="btn btn-ghost" style={{ display: 'flex', alignItems: 'center', gap: '0.6rem', padding: '0.35rem 0.85rem' }}>
              {!imgError && user?.picture ? (
                <img 
                  src={user.picture} 
                  alt="Avatar" 
                  onError={() => setImgError(true)} 
                  style={{ width: '24px', height: '24px', borderRadius: '50%', objectFit: 'cover', border: '1.5px solid var(--accent)' }} 
                />
              ) : (
                <div style={{ width: '24px', height: '24px', borderRadius: '50%', background: 'linear-gradient(135deg, var(--accent), var(--accent2))', color: '#fff', display: 'flex', alignItems: 'center', justifyContent: 'center', fontSize: '0.75rem', fontWeight: 700 }}>
                  {(user?.name ?? user?.email ?? 'U')[0].toUpperCase()}
                </div>
              )}
              {user?.name ?? user?.email}
            </Link>
            <button
              id="nav-logout"
              className="btn btn-danger"
              onClick={() => logout({ logoutParams: { returnTo: window.location.origin } })}
            >
              <IconLogout />
              Logout
            </button>
          </>
        ) : (
          <button
            id="nav-login"
            className="btn btn-primary"
            onClick={() => loginWithRedirect()}
          >
            <IconLogin />
            Sign In
          </button>
        )}
      </div>
    </nav>
  )
}
