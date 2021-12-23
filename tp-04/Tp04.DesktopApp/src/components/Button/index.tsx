import React from 'react'
import { IconType } from 'react-icons'

import * as SC from './styles'

interface IButtonProps extends React.HtmlHTMLAttributes<HTMLButtonElement> {
  icon?: IconType;
  color?: string;
}

const Button: React.FC<IButtonProps> = ({
  children,
  icon: Icon,
  color = '#6f6de8',
  ...rest
}) => {
  return (
    <SC.Container color={color} {...rest}>
      {Icon && <Icon size={24} />}

      {children}
    </SC.Container>
  )
}

export default Button
