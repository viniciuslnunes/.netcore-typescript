import React from 'react'
import { render } from 'react-dom'
import { ToastContainer } from 'react-toastify'
import { BrowserRouter } from 'react-router-dom'

import { SettingsProvider } from './hooks/settings'
import { toastContainerProps } from './config/react-toastify'

import Routes from './routes'
import { GlobalStyle } from './styles/GlobalStyle'

const mainElement = document.createElement('div')
mainElement.setAttribute('id', 'root')
document.body.appendChild(mainElement)

const App = () => {
  return (
    <BrowserRouter>
      <SettingsProvider>
        <Routes />
      </SettingsProvider>

      <GlobalStyle />
      <ToastContainer {...toastContainerProps} />
    </BrowserRouter>
  )
}

render(<App />, mainElement)
