import { RouteProps } from "react-router";

import Home from "../pages/Home";
import Settings from "../pages/Settings";
import Login from "../pages/Login";

enum RoutesEnum {
  Home = "Home",
  Login = "Login",
  Settings = "Settings",
}

interface IRouteProps extends RouteProps {
  path: string;
}

type IRouteMap = {
  [key in RoutesEnum]: IRouteProps;
};

const routesMap: IRouteMap = {
  [RoutesEnum.Home]: {
    path: "/",
    exact: true,
    component: Home,
  },
  [RoutesEnum.Login]: {
    path: "/login",
    component: Login,
  },
  [RoutesEnum.Settings]: {
    path: "/settings",
    component: Settings,
  },
};

export { routesMap };
