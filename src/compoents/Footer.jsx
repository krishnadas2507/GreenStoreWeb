import React from 'react'
import './Styles/footer.css'

function Footer() {
  return (
    <footer>
    <div className="footer-content">
      <p>&copy; {new Date().getFullYear()} Green Store. All rights reserved.</p>
    </div>
  </footer>
  )
}

export default Footer
