import { useHistory } from 'react-router'
import React, { useCallback } from 'react'
import { FiArrowLeft } from 'react-icons/fi'

import * as SC from './styles'

interface INavBarProps {
  returnTo?: string;
}

/**
 * NavBar
 * @param returnTo A route path witch return button will navigate to
 * @param children Children content, when given will be used as right content of NavBar
 * @returns ReactNode
 */
const NavBar: React.FC<INavBarProps> = ({ returnTo, children }) => {
  const history = useHistory()

  const handleGoBack = useCallback(() => {
    if (returnTo) {
      history.push(returnTo)
      return
    }

    history.goBack()
  }, [])

  return (
    <SC.Container>
      <SC.ReturnContainer onClick={handleGoBack}>
        <FiArrowLeft size={24} />

        <strong>Voltar</strong>
      </SC.ReturnContainer>

      {children}
    </SC.Container>
  )
}

export default NavBar
