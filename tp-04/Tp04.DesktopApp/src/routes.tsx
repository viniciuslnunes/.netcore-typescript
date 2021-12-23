import React from 'react'
import { HashRouter, Route } from 'react-router-dom'

import { routesMap } from './config/routes-map'

const Routes: React.FC = () => {
  return (
    <HashRouter>
      {Object.entries(routesMap).map(([routeKey, routeMap]) => (
        <Route
          key={routeKey}
          path={routeMap.path}
          exact={routeMap.exact}
          component={routeMap.component}
        />
      ))}
    </HashRouter>
  )
}

export default Routes
