import styled, { keyframes } from 'styled-components'

const spin = keyframes`
  from {
    transform: rotate(0deg);
  }

  to {
    transform: rotate(360deg);
  }
`

export const Container = styled.div`
  width: 100%;
`

export const Title = styled.div`
  color: #444;
  font-weight: 700;
  font-size: 20px;

  margin: 20px;

  svg {
    animation: ${spin} linear infinite 10s;
  }
`

export const Content = styled.div`
  width: 100%;
  margin: 20px 0;
`

export const SettingsGroup = styled.fieldset`
  position: relative;

  padding: 20px 0;
  margin: 20px 10px;

  border: 0;
  border-top: solid 1px #ddd;

  > .header {
    padding: 0 4px;

    background-color: #f2f2f2;
    position: absolute;

    transform: translate3d(0, -50%, 0);

    top: 0;
    left: 10px;
  }
`

export const TermSwitchContainer = styled.div`
  width: 100%;
  margin: 10px 0;

  display: flex;
  align-items: center;
  justify-content: center;

  svg {
    margin: 0 20px;
  }
`

export const Footer = styled.footer`
  display: flex;
  align-items: center;
  justify-content: flex-end;

  padding: 0 10px;
`
