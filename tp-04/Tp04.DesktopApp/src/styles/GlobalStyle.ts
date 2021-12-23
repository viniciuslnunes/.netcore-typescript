import 'react-toastify/dist/ReactToastify.css'
import { createGlobalStyle } from 'styled-components'

export const GlobalStyle = createGlobalStyle`
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }

  body {
    color: #808080;

    font-size: 16px;
    font-family: 'Roboto', sans-serif;
  }

  button {
    cursor: pointer;
  }

  #root {
    width: 100%;
    height: 100%;
  }
`
