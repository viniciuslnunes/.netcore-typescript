import React from "react";
import { ToastContainer } from "react-toastify";
import { BrowserRouter } from "react-router-dom";

import { toastContainerProps } from "./config/react-toastify";

import Routes from "./routes";
import { GlobalStyle } from "./styles/GlobalStyle";

const App = () => {
  return (
    <BrowserRouter>
        <Routes />

      <GlobalStyle />
      <ToastContainer {...toastContainerProps} />
    </BrowserRouter>
  );
};

export { App };
