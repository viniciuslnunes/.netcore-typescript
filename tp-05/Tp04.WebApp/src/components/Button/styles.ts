import styled from 'styled-components'
import { darken } from 'polished'

interface IContainerProps {
  color: string;
}

export const Container = styled.button<IContainerProps>`
  display: flex;
  align-items: center;

  padding: 10px;

  color: #fff;

  font-weight: 700;
  text-transform: uppercase;

  border: 0;
  border-radius: 4px;
  background-color: ${(props) => props.color};

  transition: background-color 0.25s ease;

  &:hover {
    background-color: ${(props) => darken(0.1, props.color)};
  }

  * + svg {
    margin-right: 10px;
  }
`
