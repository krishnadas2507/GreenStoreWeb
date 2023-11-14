import React from 'react'
import { Outlet } from 'react-router-dom'
import Header from './Header'
import Footer from './Footer'

function Layout() {
  return (
    <div className='main'>
    <Header/>
    <div><Outlet/></div>
    <Footer/>
</div>
  )
}

export default Layout
