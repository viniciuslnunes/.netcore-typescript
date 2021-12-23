import styled, { css } from 'styled-components'

interface IContainerProps {
  disabled: boolean;
}

export const Container = styled.div<IContainerProps>`
  width: 100%;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  padding: 20px;

  color: #444;
  text-align: center;
  font-size: 12px;
  text-transform: uppercase;
  font-weight: 700;
  background-color: #fff;

  cursor: pointer;

  box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);

  border-radius: 4px;

  transition: transform 0.25s ease;

  &:hover {
    transform: scale(1.05);
  }

  svg {
    margin-bottom: 10px;
  }

  ${(props) =>
    props.disabled &&
    css`
      background-color: #909090;

      cursor: not-allowed;

      &:hover {
        transform: unset;
      }
    `}
`
