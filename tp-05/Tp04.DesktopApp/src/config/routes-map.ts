import { RouteProps } from 'react-router'

import Home from '../pages/Home'
import Settings from '../pages/Settings'
import CheckOwner from '../pages/CheckOwner'

enum RoutesEnum {
  Home = 'Home',
  CheckOwner = 'CheckOwner',
  Settings = 'Settings',
}

interface IRouteProps extends RouteProps {
  path: string;
}

type IRouteMap = {
  [key in RoutesEnum]: IRouteProps;
};

const routesMap: IRouteMap = {
  [RoutesEnum.Home]: {
    path: '/',
    exact: true,
    component: Home
  },
  [RoutesEnum.CheckOwner]: {
    path: '/check-owner',
    component: CheckOwner
  },
  [RoutesEnum.Settings]: {
    path: '/settings',
    component: Settings
  }
}

export { routesMap }
