import React from 'react'
import { IconType } from 'react-icons/lib'

import * as SC from './styles'

interface IInputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  icon?: IconType;
}

type InputType = React.ForwardRefExoticComponent<
  IInputProps & React.RefAttributes<HTMLInputElement>
>;

const Input: InputType = React.forwardRef(function Input (
  { icon: Icon, ...rest },
  forwardRef
) {
  return (
    <SC.Container>
      {Icon && <Icon size={24} />}
      <SC.InputElement ref={forwardRef} {...rest} />
    </SC.Container>
  )
})

export default Input
